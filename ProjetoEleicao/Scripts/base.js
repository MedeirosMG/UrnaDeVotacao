//custom alerts
var CustomAlert = function () {

    this.delayModalClose = function (delayMillis, _modalName) {
        //1000 = 1 second
        setTimeout(function () {
            $("#" + _modalName).modal("hide");
            //your code to be executed after 1 second
        }, delayMillis);
    }
    

    this.alertWarning = function (_msg) {
        $.alert({
            alignMiddle: false,
            offsetTop: 120,
            backgroundDismiss: true,
            //title: $v2.translate("Attention"),
            title: "Atenção",
            titleClass: 'msg-warning',
            content: _msg,
            icon: 'fa fa-exclamation-triangle',
            closeIcon: true,
            draggable: false,
            type: 'orange',
            buttons: {
                Ok: {
                    text: 'Ok',
                    btnClass: 'btn-orange'
                }
            }
        });
    }

    this.alertSuccess = function(_title, _msg) {
        $.alert({
            alignMiddle: false,
            offsetTop: 120,
            title: _title,
            titleClass: 'msg-success',
            content: _msg,
            icon: 'fa fa-check',
            closeIcon: true,
            type: 'green',
            backgroundDismiss: false,
            backgroundDismissAnimation: 'glow-green',
            buttons: {
                Ok: {
                    text: 'Ok',
                    btnClass: 'btn-green'
                }
            }
        });
    }

    this.alertError = function (_title, _msg) {
        $.alert({
            alignMiddle: false,
            offsetTop: 120,
            backgroundDismiss: false,
            backgroundDismissAnimation: 'glow',
            //title: $v2.translate("Error"),
            title: _title,
            titleClass: 'msg-error',
            content: _msg,
            icon: 'fa fa-times',
            closeIcon: true,
            draggable: false,
            type: 'red',
            buttons: {
                Ok: {
                    text: 'Ok',
                    btnClass: 'btn-red'
                }
            }
        });
    }

    this.confirmError = function (_title, _msg, _function, _parameter) {
        $.confirm({
            alignMiddle: false,
            offsetTop: 120,
            backgroundDismiss: false,
            backgroundDismissAnimation: 'glow',
            //title: $v2.translate("Error"),
            title: _title,
            titleClass: 'msg-error',
            content: _msg,
            icon: 'fa fa-times',
            closeIcon: true,
            draggable: false,
            type: 'red',
            buttons: {
                ok: function () {
                    _function(_parameter);
                },
                cancel: function () {
                }
            }
        });
    }

    this.confirmWarning = function (_title, _msg, _function, _parameter) {
        $.confirm({
            alignMiddle: false,
            offsetTop: 120,
            backgroundDismiss: true,
            //title: $v2.translate("Attention"),
            title: _title,
            titleClass: 'msg-warning',
            content: _msg,
            icon: 'fa fa-exclamation-triangle',
            closeIcon: true,
            draggable: false,
            type: 'orange',
            buttons: {
                ok: function () {
                    _function(_parameter);
                },
                cancel: function () {
                }
            }
        });
    }

    this.confirmSuccess = function (_title, _msg, _function, _parameter) {
        $.confirm({
            alignMiddle: false,
            offsetTop: 120,
            title: _title,
            titleClass: 'msg-success',
            content: _msg,
            icon: 'fa fa-check',
            closeIcon: true,
            type: 'green',
            backgroundDismiss: false,
            backgroundDismissAnimation: 'glow-green',
            buttons: {
                ok: function () {
                    _function(_parameter);
                },
                cancel: function () {
                }
            }
        });
    }
}

//order by column
ko.bindingHandlers.sort = {
    init: function (element, valueAccessor) {
        var asc = false;

        ko.utils.registerEventHandler(element, 'click', function (event) {
            var value = ko.utils.unwrapObservable(valueAccessor()),
                sortBy = event.target.getAttribute('data-sortBy'),
                list = value.list;

            //Verificando se existe lista na tabela
            if (value.list().length > 0) {
                var elementIcon;
                if (event.target.tagName == "I") {
                    elementIcon = $(event.target);
                    sortBy = event.target.parentNode.getAttribute('data-sortBy');
                } else if (event.target.tagName == "TH") {
                    elementIcon = $(event.target).children('i');
                }
                $(elementIcon).closest("tr").children().each(function (i, obj) {
                    //Limpando filtro das outras colunas
                    $(obj).children('i').removeClass();
                });
                //Alterando icone, não dá erro caso nao exista o icone
                if (elementIcon != undefined) {
                    elementIcon.removeClass();
                    //elementIcon.addClass(asc ? "icon-sort-up" : "icon-sort-down");
                    elementIcon.addClass(asc ? "glyphicon glyphicon-menu-up" : "glyphicon glyphicon-menu-down");
                }

                asc = !asc;
                list.sort(function (left, right) {
                    //
                    var leftValue = eval('left.' + sortBy),
                        rightValue = eval('right.' + sortBy);
                    // avoid problem on minify
                    left = null; right = null;
                    if (typeof leftValue === 'string' && typeof rightValue === 'string') {
                        leftValue = leftValue.toLowerCase();
                        rightValue = rightValue.toLowerCase();
                    }

                    var x = 0;
                    if (leftValue < rightValue) {
                        x = -1;
                    } else if (leftValue > rightValue) {
                        x = 1;
                    }
                    x = asc ? x : -1 * x;
                    return x;
                });
            }
        });
    }
};

//checked btn-group get value
ko.bindingHandlers.bsChecked = {
    init: function (element, valueAccessor, allBindingsAccessor,
        viewModel, bindingContext) {
        var value = valueAccessor();
        var newValueAccessor = function () {
            return {
                change: function () {
                    value(element.value);
                }
            }
        };
        ko.bindingHandlers.event.init(element, newValueAccessor,
            allBindingsAccessor, viewModel, bindingContext);
    },
    update: function (element, valueAccessor, allBindingsAccessor,
        viewModel, bindingContext) {
        if ($(element).val() == ko.unwrap(valueAccessor())) {
            setTimeout(function () {
                $(element).closest('.btn').button('toggle');
            }, 1);
        }
    }
}
