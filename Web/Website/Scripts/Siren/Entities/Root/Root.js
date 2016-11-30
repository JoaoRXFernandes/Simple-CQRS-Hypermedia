Root.prototype = Object.create(SirenEntity.prototype);
Root.prototype.constructor = Root;

function Root(sirenResponse, entityAddress) {
    this.subscriptions = [];

    this.entityAddress = ko.observable(entityAddress);
    
    ko.mapping.fromJS(sirenResponse, SirenMappings.prototype.EntityMapping, this);

    this.subscriptions.push(postbox.subscribe(this.handleResponse.bind(this), null, this.href()));

    this.menu = new Menu(this.links, this.actions);
    this.menu.executeLink(_.first(this.menu.links()));

    return this;
};