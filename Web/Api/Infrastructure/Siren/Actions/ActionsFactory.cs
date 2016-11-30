using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using Nancy;
using Nancy.Helpers;
using Web.Api.Infrastructure.Extensions;
using Web.Api.Infrastructure.Requests.Actions;
using Web.Api.Infrastructure.Siren.Siren;
using Web.Api.Infrastructure.Siren.Siren.Extensions;
using Action = Web.Api.Infrastructure.Siren.Siren.Action;

namespace Web.Api.Infrastructure.Siren.Actions
{
    public class ActionsFactory
    {
        private readonly NancyContext _context;
        private readonly IList<Action> _actions;

        public ActionsFactory(NancyContext context)
        {
            _context = context;
            _actions = new List<Action>();
        }

        public IList<Action> Build()
        {
            return _actions;
        }

        public ActionsFactory With<T>(T request, params IWithAction<T>[] overrides) where T : ApiAction
        {
            var href = request.Href;
            IList<Field> fields = null;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (!property.CanRead)
                    continue;

                var propertyGetMethod = property.GetGetMethod(false);
                var propertySetMethod = property.GetSetMethod(false);
                if (propertyGetMethod == null || propertySetMethod == null)
                    continue;

                var value = property.GetValue(request, null) ?? DefaultOf(property.PropertyType);

                var propertyName = property.Name.FormatAsName();
                var propertyValueTemplate = "{" + propertyName + "}";

                if (href.Contains(propertyValueTemplate))
                {
                    href = href.Replace(propertyValueTemplate, HttpUtility.UrlEncode(value.ToString()));
                }
                else
                {
                    if (fields == null)
                        fields = new List<Field>();

                    var field = new Field(property.Name, "string", value, IsRequired<T>(property.Name));

                    var fieldOverrides = overrides.Where(x => x is IHavingActionFieldOverride<T>).Cast<IHavingActionFieldOverride<T>>().SingleOrDefault(x => x.GetFieldName() == property.Name);
                    if (fieldOverrides != null)
                        fieldOverrides.Apply(field);

                    if (field.Type != "hidden")
                    {
                        if (property.PropertyType == typeof(int))
                        {
                            field.Type = "number";
                            field.Step = 1;

                            if (field.Min == null)
                            {
                                var minValue = MinValue<T>(propertyName);
                                field.Min = minValue;

                                if (field.Value == null || (int) field.Value == 0)
                                    field.Value = minValue;
                            }
                        }
                        else if (property.PropertyType == typeof(decimal))
                        {
                            field.Type = "number";
                            field.Step = 0.1;

                            if (field.Min == null)
                            {
                                var minValue = MinValue<T>(propertyName);
                                field.Min = minValue;

                                if (field.Value == null || (decimal)field.Value == 0)
                                    field.Value = minValue;
                            }
                        }
                    }
                    
                    


                    

                    fields.Add(field);
                }
            }

            var action = new Action(request.Title, request.Method, _context.GetFullUrlFor(href), fields);
            var actionOverrides = overrides.Where(x => x is IHavingActionOverride<T>).Cast<IHavingActionOverride<T>>();
            foreach (var @override in actionOverrides)
                @override.Apply(action);

            _actions.Add(action);

            return this;
        }

        private static bool IsRequired<T>(string propertyName) where T : ApiAction
        {
            var isRequired = false;

            var validatorType = AppDomain.CurrentDomain.GetAssemblies()
                                            .SelectMany(s => s.GetTypes())
                                            .SingleOrDefault(p => typeof(AbstractValidator<T>).IsAssignableFrom(p));

            if (validatorType == null)
                return false;

            var requestValidator = (AbstractValidator<T>)Activator.CreateInstance(validatorType);
            foreach (var property in requestValidator)
            {
                if ((property as FluentValidation.Internal.PropertyRule).PropertyName.ToLower() == propertyName.ToLower())
                {
                    foreach (var propertyValidator in property.Validators)
                    {
                        if (propertyValidator.GetType() == typeof(FluentValidation.Validators.NotNullValidator) || propertyValidator.GetType() == typeof(FluentValidation.Validators.NotEmptyValidator))
                        {
                            isRequired = true;
                        }
                    }
                }
            }

            return isRequired;
        }

        private static object MinValue<T>(string propertyName) where T : ApiAction
        {
            var validatorType = AppDomain.CurrentDomain.GetAssemblies()
                                            .SelectMany(s => s.GetTypes())
                                            .SingleOrDefault(p => typeof(AbstractValidator<T>).IsAssignableFrom(p));

            if (validatorType == null)
                return false;

            var requestValidator = (AbstractValidator<T>)Activator.CreateInstance(validatorType);
            foreach (var property in requestValidator)
            {
                if ((property as FluentValidation.Internal.PropertyRule).PropertyName.ToLower() == propertyName.ToLower())
                {
                    foreach (var propertyValidator in property.Validators)
                    {
                        if (propertyValidator.GetType() == typeof(FluentValidation.Validators.GreaterThanOrEqualValidator))
                        {
                            return ((FluentValidation.Validators.GreaterThanOrEqualValidator) propertyValidator).ValueToCompare;
                        }
                    }
                }
            }

            return null;
        }

        private object DefaultOf(Type t)
        {
            return this.GetType().GetMethod("GetDefaultGeneric").MakeGenericMethod(t).Invoke(this, null);
        }

        public T GetDefaultGeneric<T>()
        {
            return default(T);
        }
    }
}
