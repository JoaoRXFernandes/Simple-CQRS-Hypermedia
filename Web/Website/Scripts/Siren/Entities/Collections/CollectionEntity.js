CollectionEntity.prototype = Object.create(SirenEntity.prototype);
CollectionEntity.prototype.constructor = CollectionEntity;

function CollectionEntity(sirenResponse, entityAddress) {
    this.subscriptions = [];
    this.entityAddress = ko.observable(entityAddress);

    ko.mapping.fromJS(sirenResponse, SirenMappings.prototype.EntityMapping, this);

    this.title = ko.observable(_.first(this.class()));

    this.activeAction = ko.observable();

    this.subscriptions.push(postbox.subscribe(this.handleResponse.bind(this), null, this.href()));
    
    return this;
};