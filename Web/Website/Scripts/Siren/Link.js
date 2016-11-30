function SirenLink(sirenLink, entityAddress) {
    this.entityAddress = ko.observable(entityAddress);

    ko.mapping.fromJS(sirenLink, null, this);

    return this;
};

SirenLink.prototype.execute = function (callback) {
    this.get(this.href(), callback);
};


SirenLink.prototype.get = function (href) {
    $.ajax({
        type: "GET",
        url: href,
        success: this.handleSuccess.bind(this),
        error: this.handleError.bind(this),
        dataType: "json"
    });
};


SirenLink.prototype.handleSuccess = function (data) {
    postbox.notifySubscribers(data, this.entityAddress());
};

SirenLink.prototype.handleError = function (jqXHR, textStatus, errorThrown) {
    postbox.notifySubscribers(JSON.parse(jqXHR.responseText), this.entityAddress());
};