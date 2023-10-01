function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}

function setButton() {
    var btn = document.getElementById("Button");
    btn.disabled = true;
}

function formValidateEmail() {
    var regExp = /^[A-Za-z][\w$.]+@[\w]+\.\w+$/;
    var email = document.getElementById("email").value;
    if (regExp.test(email))
        alert('Email hợp lệ!');
    else
        alert('email không hợp lệ!');
}
function formValidateSDT() {
    var regExp = /^(0[234][0-9]{8}|1[89]00[0-9]{4})$/;
    var phone = document.getElementById("phone").value;
    if (regExp.test(phone))
        alert('SĐT hợp lệ!');
    else
        alert('SĐT không hợp lệ!');
}