var Chicadresse = Chicadresse || {};
Chicadresse.GuestList = {};

var Invites = Invites || {};
Invites.OpenModal = function () {
    $("#chicadresse-modal").modal(true);
}
Invites.CloseModal = function () {
    $("#chicadresse-modal").modal('hide');
}

$(function () {
    Chicadresse.GuestList.init();
})

Object.defineProperties(Chicadresse.GuestList, {
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
            $()
            $('#click_advance').click(function () {
                $('#display_advance').toggle('1000');
                $("i", this).toggleClass("fa-plus fa-minus");
            });

            $(document).on('change', '.presence.selectpicker', function () {
                debugger;
                var presenceId = $(this).val();
                var guestId = $(this).data("id");
                var url = $(this).data("url");
                var data = { presenceId: presenceId, guestId: guestId };
                that.sendRequest(data, url, function () {

                });
            })

            $(document).on('change', '.groups.selectpicker', function () {
                var groupId = $(this).val();
                var guestId = $(this).data("id");
                var url = $(this).data("url");
                var data = { groupId: groupId, guestId: guestId };
                that.sendRequest(data, url, function () {

                });
            })

            $(document).on('change', '.tables.selectpicker', function () {
                var tableId = $(this).val();
                var guestId = $(this).data("id");
                var url = $(this).data("url");
                var data = { tableId: tableId, guestId: guestId };
                that.sendRequest(data, url, function () {

                });
            })
        }
    },
    "sendRequest": {
        value: function (data, url, callback) {
            $.ajax({
                type: "POST",
                data: data,
                url: url,
                success: callback
            })
        }
    }
});
