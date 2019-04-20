<script>
    function saveChange() {
        //debugger;
    var oldpass = $("input[name='current_password']").val();
    var new_password = $("input[name='new_password']").val();
    var comfirmpass = $("input[name='confirm_password']").val();
    var makhach = $("input[name='MaKhach']").val();
    var username = $("input[name='user_name']").val();

        if (oldpass == null || oldpass=="") {
        $(".mess_old").text("Bạn phải nhập trường này");
    return false;
}
        if (new_password == null || new_password == "") {
        $(".mess_new").text("Bạn phải nhập trường này");
    return false;
}
        if (comfirmpass == null || comfirmpass == "") {
        $(".mess_con").text("Bạn phải nhập trường này");
    return false;
}
        if (new_password != comfirmpass) {
        alert("Mật khẩu và xác thực mật khẩu không khớp nhau!");
    return false;
}
        $.ajax({
        url: '@Url.Action("ChangePass", "Account")',
            data: {
        oldpass: oldpass,
    newpass: new_password,
    username: username,
    makhach: makhach,
},
type: 'post',
dataType: 'json',
traditional: true,
            success: function (data) {
                //debugger;
                if (data.Success) {
        //alert(data.Message);
        $(".message").text(data.Message);
    //window.location.href = window.location.href;
    } else {
        $(".message").text(data.Message);
    //window.location.href = url;
    }
},
});
}
    function saveEdit() {
        debugger;
    var email = $("input[name='email']").val();
    var address = $("input[name='address']").val();
    var fullname = $("input[name='fullname']").val();
    var makhach = $("input[name='MaKhach']").val();
    var phone = $("input[name='phone']").val();
    var username = $("input[name='user_name']").val();
        var reg = /^([A-Za-z0-9_\-\.])+\@@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

        if (fullname == null || fullname == "") {
        $(".mess_full").text("Bạn phải nhập trường này");
    return false;
}
        if (address == null || address == "") {
        $(".mess_address").text("Bạn phải nhập trường này");
    return false;
}
        if (email == null || email == "") {
        $(".mess_email").text("Bạn phải nhập trường này");
    return false;
}
        if (reg.test(email)) {
        $(".mess_email").text("Email sai định dạng!");
    }
   
        if (phone == null || phone == "") {
        $(".mess_phone").text("Bạn phải nhập trường này");
    return false;
}

        $.ajax({
        url: '@Url.Action("Update", "Account")',
            data: {
        makhach: makhach,
    fullname: fullname,
    address: address,
    phone: phone,
    email: email,
},
type: 'post',
dataType: 'json',
traditional: true,
            success: function (data) {
                //debugger;
                if (data.Success) {
        //alert(data.Message);
        $(".message").text(data.Message);
    window.location.href = window.location.href;
                } else {
        $(".message").text(data.Message);
    //window.location.href = url;
    }
},
});
}
</script>