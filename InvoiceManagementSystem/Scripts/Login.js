function ShowWait() {
    $('#divloader').show();
}
function HideWait() {
    $('#divloader').hide();
}

function SignUp() {

    var val = true;
    var UserName = $('#UserName').val();
    if (UserName == "" || /\S/.test(UserName) == false) {
        $("#errName").html("Please enter username.");
        val = false;
    }

    var Email = $('#Email1').val();
    if (Email == '' || Email.trim() == '') {
        $("#errEmail").html('Please enter email id.');
        val = false;
    }

    //var atpos = Email.indexOf("@@");
    //var dotpos = Email.lastIndexOf(".");
    //if (atpos < 1 || dotpos  < atpos + 2 || dotpos + 2 >= Email.length) {
    //    $("#errEmail").html("Please enter valid email id.");
    //    val = false;
    //}
    var Password = $('#Password1').val();
    if (Password == "" || /\S/.test(Password) == false) {
        $("#errPassword").html("Please enter password.");
        val = false;
    }
    if (val == false) {
        return;
    }
    var cls = {
        UserName: UserName,
        Email: Email,
        Password: Password,
    }
    ShowWait();
    $.ajax({
        type: "POST",
        url: '/Account/SignUp',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({
            cls: cls
        }),
        success: function (data) {
            if (data != null) {
                if (data == 'Success') {
                    toastr.success('User registered inserted successfully');
                    $('.login').click();
                    //window.location.href = '/Home/Index';
                }
                else if (data == 'Exists') {
                    toastr.error('Email already exists!');
                    document.getElementById('Email').value = "";
                }
            }
            HideWait();
        },
        error: function (xyz) {
            HideWait();
            alert('errors');
        }
    });
}

function Login() {
    debugger
    var val = true;
    var UserName = document.getElementById('UserName').value;
    var Password = document.getElementById('Password').value;
    if (UserName == "" || /\S/.test(UserName) == false) {
        $("#errUserName").html("Please Enter Username.");
        val = false;
    }
    if (Password == "" || /\S/.test(Password) == false) {
        $("#errPassword").html("Please Enter Password.");
        val = false;
    }
    if (val == false) {
        return;
    }

    var cls = {
        UserName: UserName,
        Password: Password
    }
    $.ajax({
        url: '/Account/Login',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        data: JSON.stringify({
            cls: cls
        }),
        success: function (data) {

            if (data == 'Success') {
                window.location.href = '/Home/Index';

            }
            //else if (data.intId == 0) {
            //    alert(data.strResponse);
            //}
            else if (data == 'Error' || data == null) {
                toastr.error('Invalid Username or Password.');
            }
            document.getElementById('Email').value = '';
            document.getElementById('Password').value = '';
        },
        error: function (xhr) {

            alert('errors');
        }
    });
}

function MyProfile() {
    ShowWait();
    $.ajax({
        url: '/Account/GetMyProfile',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        data: {},
        success: function (data) {
            if (data != null) {
                document.getElementById('hdnintId').value = data.LSTAccountList[0].Id;
                document.getElementById('FullName').value = data.LSTAccountList[0].FullName;
                document.getElementById('Email').value = data.LSTAccountList[0].Email;
                document.getElementById('MobileNo').value = data.LSTAccountList[0].Mobile;
                document.getElementById('Address').value = data.LSTAccountList[0].Address;
                document.getElementById('UserName').value = data.LSTAccountList[0].UserName;
            }
            else {
                alert('error');
            }
            HideWait();

        },
        error: function (xhr) {
            HideWait();
            alert('errors');
        }
    });
}


function UpdateProfile(id) {

    var Id = id
    var UserName = document.getElementById('UserName').value;
    var FullName = document.getElementById('FullName').value;
    var Email = document.getElementById('Email').value;
    var MobileNo = document.getElementById('MobileNo').value;
    var Address = document.getElementById('Address').value;

    var cls = {
        Id: Id,
        UserName: UserName,
        FullName: FullName,
        Email: Email,
        Mobile: MobileNo,
        Address: Address

    }
    ShowWait()
    $.ajax({
        url: '/Account/UpdateProfile',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        data: JSON.stringify({
            cls: cls
        }),
        success: function (data) {

            if (data != null) {
                if (data.Response == 'Success') {
                    toastr.success('Profile updated successfully');
                    MyProfile();
                }
            }
            HideWait();
        },
        error: function (xhr) {
            HideWait();
            alert('errors');
        }
    });
}


function ChangePassword() {
    var newPassword = document.getElementById('newPassword').value;
    var cls = {
        Password: newPassword
    }
    ShowWait()
    $.ajax({
        url: '/Account/UpdatePassword',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        data: JSON.stringify({
            cls: cls
        }),
        success: function (data) {

            if (data != null) {
                if (data.Response == 'Success') {
                    toastr.success('Profile updated successfully');
                    MyProfile();
                }
            }
            HideWait();
        },
        error: function (xhr) {
            HideWait();
            alert('errors');
        }
    });
}


function showPwd() {
    var x = document.getElementById("Password");

    if (x.type === "password") {
        x.type = "text";
    }
    else {
        x.type = "password";
    }
}

function Logout() {

    $.ajax({
        url: '/Account/Logout',
        contentType: "application/json; charset=utf-8",
        type: "POST",
        data: JSON.stringify({

        }),
        success: function (data) {
            window.location.href = '/Account/Login';

        },
        error: function (xhr) {
            alert('errors');
        }
    });
}