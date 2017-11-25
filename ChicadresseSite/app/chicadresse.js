var Chicadresse = Chicadresse || {};

Chicadresse.Utility = function () {

}

if (typeof jQuery !== "undefined") {
    $(function () {

        $(document).on('change', 'select', function () { $(this).trigger('focusout'); }); // fix bootstrap-select 

        // parse form if any ajax request's response contains form
        $(document).ajaxStop(function () {
            $.validator.unobtrusive.parse(document);
            $('.selectpicker').selectpicker();
        });

        //validators default
        $.validator.setDefaults({
            errorPlacement(error, element) {
                error.insertAfter(element.parent('.input-group').length || element.prop('type') === 'checkbox' || element.prop('type') === 'radio' ? element.parent() : element);
            },
            highlight(element) {
                let $element = $(element),
                    $formGroup = $element.closest('.form-group');
                $formGroup.addClass('has-error');
            },
            unhighlight(element) {
                let $element = $(element),
                    $formGroup = $element.closest('.form-group');

                $formGroup.removeClass('has-error');
            }
        });
    });
}