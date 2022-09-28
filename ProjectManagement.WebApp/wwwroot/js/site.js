function loadModal(url) {
    $.get(url, function (data) {
        var dataContent = $($.parseHTML(data)).get();
        var modalId = '';
        for (var i = 0; i < dataContent.length; i++) {
            if (dataContent[i].className == 'modal') {
                modalId = $(dataContent[i]).attr('id');
                break;
            }
        }
        $('#modalloader').append(data);
        $('#modalloader').find('#' + modalId).modal('show');

        $('[data-dismiss="modal"]').on('click', function () {
            $(this).parents('.modal').modal('dispose').remove();
            $('#modalloader').html('');
        });
    });
}

function closeAllModals() {
    $('#modalloader').find('.modal').modal('hide').remove();
    $('#modalloader').empty();
}

function showError(msg) {
    var modalId = 'alertmodal_' + parseInt(Math.random() * 10000);
    var data =
        `<div class="modal" id=${modalId} tabindex="-1" role="dialog">
          <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
              <div class="modal-header bg-danger text-white">
                <h5 class="modal-title">Error</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <p>${msg}</p>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
              </div>
            </div>
          </div>
        </div>`;

    $('#modalloader').append(data);
    $('#modalloader').find('#' + modalId).modal('show');

    $('[data-dismiss="modal"]').on('click', function () {
        $(this).parents('.modal').modal('dispose').remove();
    });
}

function showSuccessToast(msg) {
    $('#successToast').find('.toast-body').text(msg);
    $('#successToast').toast('show');
}