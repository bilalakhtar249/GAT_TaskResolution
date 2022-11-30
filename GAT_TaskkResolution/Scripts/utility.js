$('.alert').on('close.bs.alert', function (e) {
    e.preventDefault();
    $(this).addClass('hidden');
});