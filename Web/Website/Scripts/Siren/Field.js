function SirenField(sirenField) {
    this.subscriptions = [];
    
    this.value = ko.observable();
    
    ko.mapping.fromJS(sirenField, {}, this);

    if (sirenField.required === true) {
        this.value.extend({ required: true });
    }
    
    if (sirenField.type === "email") {
        this.value.extend({ email: true });
    }

    if (sirenField.type === "number") {
        this.value.extend({ number: true });

        if (sirenField.min) {
            this.value.extend({ min: sirenField.min });
        }
    
        if (sirenField.step) {
            this.value.extend({ step: sirenField.step });
        }
    }

    if (sirenField.type === "select") {
        if (field.optionsLookup) {
            $.ajax({
                type: "GET",
                url: field.optionsLookup.href,
                success: this.loadOptions.bind(this)
            });
        }
    }

    return this;
};


SirenField.prototype.loadOptions = function (response) {
    var options = [];

    for (var key in response.properties) {
        if (response.properties.hasOwnProperty(key)) {
            options.push({ key: key, value: response.properties[key] });
        }
    }

    this.options(options);
};


SirenField.prototype.dispose = function () {
    ko.utils.arrayForEach(this.subscriptions, this.disposeEach);
    ko.utils.objectForEach(this, this.disposeEach);
};

SirenField.prototype.disposeEach = function (propOrValue, value) {
    var disposable = value || propOrValue;

    if (disposable && typeof disposable.dispose === "function") {
        disposable.dispose();
    }
};