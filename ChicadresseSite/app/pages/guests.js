$(function () {
    $('.selectpicker').selectpicker();
    Chicadresse.Guest.init();
})

var Chicadresse = Chicadresse || {};
Chicadresse.Guest = {
    companions: [],
};

Chicadresse.GuestCompanions = [
    {
        placeholder: "Nom",
        name: "FirstName",
        controlType: "text",
        validation: ["required"]
    },
    {
        placeholder: "Prenom",
        name: "LastName",
        controlType: "text",
        validation: ["required"]
    }];

Object.defineProperties(Chicadresse.Guest, {
    "companionsCount": {
        value: 0,
        configurable: false,
        writable: true
    },
    "init": {
        writable: false,
        value: function () {
            this.events();
        }
    },
    "events": {
        value: function () {
            var that = this;
            $("#click_advance").on("click", function () {
                var $row = $("<div>").addClass("row");
                var template = "";
                $.each(Chicadresse.GuestCompanions, function (index, item) {
                    if (index == 0) {
                        template += `<input type="hidden" name="Companions.Index" value="${that.companionsCount}">
                           <div class="row-remove"> <i class="fa fa-minus"></i></div>`;
                    }
                    template += `<div class="form-group col-sm-6">
                    <input type="text" data-val="true" data-val-required="The ${item.name} field is required." id="Companions_${that.companionsCount}_${item.name}" class="form-control" name="Companions[${that.companionsCount}].${item.name}" placeholder="${item.placeholder}" />
                    <span class="field-validation-valid" data-valmsg-for="Companions[${that.companionsCount}].${item.name}" data-valmsg-replace="true"></span>
                </div>`
                });

                $row.html(template);
                var $display = $("#display_advance");
                $display.append($row);
                $.validator.unobtrusive.parseDynamicContent($display);
                that.companionsCount++;
            });

            let $display = $("#display_advance");
            that.companionsCount = $display.find(".row-remove").length;
            $display.on('click', '.row-remove', that.removeRow);
        }
    },
    "removeRow": {
        value: function () {
            var $display = $("#display_advance");
            let $row = $(this).closest(".row");
            $row.remove()
            if ($display.find(".row").length == 0) {
                Chicadresse.Guest.companionsCount = 0;
            }
        }
    }
});
