﻿/*breadcrumb*/
var url = window.location.href.toLowerCase();

var breadcrumb = [];
breadcrumb.push({ title: "پنل ادمین", link: "/Admin/Dashboard" });




// عملیات مربوط به مدیریت پرسنل
// آیا سابمیت آغاز شده است؟
var startSubmition = false;
var user = {

    // لیست کاربر ها
    list: {
        // آبجکت دیتاتیبل
        table: null,


        // راه اندازی دیتاتیبل
        initial: function () {
            this.table = $('#datatables').DataTable({
                "drawCallback": function (settings) {
                    $('[data-toggle="tooltip"]').tooltip();
                },
                language: {
                    url: "/assets/datatables/fa-lang.json"
                },
                "pagingType": "full_numbers",
                "lengthMenu": [
                    [10, 25, 50, -1],
                    [10, 25, 50, "All"]
                ],
                responsive: true,
                "ajax": {
                    "url": "/AuthSystem/Users/GetList",
                    "type": "POST",
                    "dataType": "json",
                    "data": function (d) {
                        return $.extend({}, d, filter.collect());
                    },
                },
                "columns": [
                    { "data": "id", "name": "شناسه" },
                    { "data": "fullName", "name": "نام کامل" },
                    { "data": "username", "name": "نام کاربری" },
                    { "data": "mobile", "name": "تلفن" },
                    {
                        data: "roleId",
                        render: function (data, type, row) {
                            return row.role;
                        }
                    },
                    {
                        data: "isEnabled",
                        render: function (data, type, row) {
                            var checked = '';
                            if (data) checked = 'checked';
                            return '<label class="toggle for-isEnabled"><input onchange="user.toggleEnable(this, ' + row.id + ')" type="checkbox" ' + checked + '><span class="slider"></span><span class="labels" data-on="فعال" data-off="غیرفعال"></span></label>';
                        }
                    },
                    {
                        data: null,
                        className: "text-left",
                        render: function (data, type, row) {
                            var btns = "<a onclick='user.edit.loadForm(" + data.id + ")' class='btn btn-simple btn-info btn-icon' title='ویرایش' data-toggle='tooltip'><i class='material-icons'>edit</i></a>"
                                //+ "<a onclick='user.changePassword.loadForm(" + data.id + ")' class='btn btn-simple btn-primary btn-icon' title='تغییر کلمه عبور' data-toggle='tooltip'><i class='material-icons'>lock</i></a>"
                                //+ "<a onclick='user.resetPassword.loadForm(" + data.id + ")' class='btn btn-simple btn-primary btn-icon' title='ریست کردن کلمه عبور' data-toggle='tooltip'><i class='material-icons'>lock</i></a>"
                                + "<a onclick='user.delete.loadForm(" + data.id + ")' class='btn btn-simple btn-danger btn-icon' title='حذف' data-toggle='tooltip'><i class='material-icons'>close</i></a>";

                            return btns;
                        }
                    }
                ],
                "serverSide": "true",
                "order": [0, "desc"],
                "processing": "true",
                'columnDefs': [{
                    'targets': [1, 2, 3, 4, 5, 6], /* column index */
                    'orderable': false, /* true or false */
                }]

            });
        },


        //رفرش کردن دیتا تیبل
        reload: function () {
            user.list.table.ajax.reload(function () {
                //$.material.init();
            }, false);
        }
    },



    // آماده سازی فرم ها
    form: {
        initial: function () {
            $('#base-modal').removeClass('small-modal');
            $('.selectpicker').selectpicker('refresh');
            $.material.init();
        },

    },



    // افزودن پرسنل جدید
    create: {
        // لود کردن فرم افزودن پرسنل جدید
        loadForm: function () {
            $.get("/AuthSystem/users/LoadCreateForm",
                function (res) {
                    $("#modal-form").html(res);
                    user.form.initial();
                    modal.open();

                    /*اعمال ولیدیشن به فرمی که با اجکس لود شده است*/
                    var form = $(".create-form")
                        .removeData("validator")
                        .removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse(form);
                });
        },



        // ذخیره اطلاعات پرسنل جدید
        save: function (e) {
            if (startSubmition == true)
                return false;
            startSubmition = true;

            e.preventDefault();
            var targetUrl = $(".create-form").attr("action");

            /*ولیدیت کردن فرم*/
            var $form = $("form");
            $form.validate();
            if (!$form.valid()) {
                startSubmition = false;
                return false;
            }

            /*جمع آوری دیتای فرم*/
            var data = $(".create-form").serialize();

            /*ثبت فرم با اجکس*/
            $.post(targetUrl,
                data,
                function (res) {
                    startSubmition = false;
                    if (res.status) {
                        user.list.reload();
                        modal.close();
                    }
                    else {
                        $(".create-form .error").html(res.message);
                        setScrollPosition();
                    }
                }).fail(function () {
                    startSubmition = false;
                    $(".create-form .error").html("ذخیره اطلاعات با خطا همراه بوده است. مجددا اقدام کنید.");
                    setScrollPosition();
                });
            return false;
        }
    },



    // ویرایش پرسنل
    edit: {
        // لود کردن فرم ویرایش پرسنل
        loadForm: function (id) {
            $.get("/AuthSystem/users/LoadEditForm/" + id,
                function (res) {
                    $("#modal-form").html(res);
                    user.form.initial();
                    modal.open();

                    /*اعمال ولیدیشن به فرمی که با اجکس لود شده است*/
                    var form = $(".edit-form")
                        .removeData("validator")
                        .removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse(form);
                });
        },



        // ذخیره اطلاعات ویرایش شده
        save: function () {
            if (startSubmition == true)
                return false;
            startSubmition = true;

            var targetUrl = $(".edit-form").attr("action");

            /*ولیدیت کردن فرم*/
            var $form = $("form");
            $form.validate();
            if (!$form.valid()) {
                startSubmition = false;
                return false;
            }

            /*جمع آوری دیتای فرم*/
            var data = $(".edit-form").serialize();

            /*ثبت فرم با اجکس*/
            $.post(targetUrl,
                data,
                function (res) {
                    startSubmition = false;
                    if (res.status) {
                        user.list.reload();
                        modal.close();
                    }
                    else {
                        $(".error").html(res.message);
                        setScrollPosition();
                    }
                }).fail(function () {
                    startSubmition = false;
                    $(".error").html("ذخیره اطلاعات با خطا همراه بوده است. مجددا اقدام کنید.");
                    setScrollPosition();
                });
            return false;
        },

    },




    editProfile: {
        save: function () {
            if (startSubmition == true)
                return false;
            startSubmition = true;

            var targetUrl = $(".edit-form").attr("action");

            /*ولیدیت کردن فرم*/
            var $form = $("form");
            $form.validate();
            if (!$form.valid()) {
                startSubmition = false;
                return false;
            }

            /*جمع آوری دیتای فرم*/
            var data = $(".edit-profile-form").serialize();

            /*ثبت فرم با اجکس*/
            $.post(targetUrl,
                data,
                function (res) {
                    startSubmition = false;
                    if (res.status) {
                        $(".success").html('ذخیره تغییرات با موفقیت انجام شد.');
                    }
                    else {
                        $(".error").html(res.message);
                        setScrollPosition();
                    }
                }).fail(function () {
                    startSubmition = false;
                    $(".error").html("ذخیره اطلاعات با خطا همراه بوده است. مجددا اقدام کنید.");
                    setScrollPosition();
                });
            return false;
        }
    },


    // تغییر کلمه عبور کاربر
    profileChangePassword: {

        isPasswordStrength: function (password) {
            var password_strength = $("#password-text");
            
            //TextBox left blank.
            if (password.length == 0) {
                password_strength.removeClass('btn-warning btn-success btn-danger').css('width', '1%');
                return false;
            }

            //Regular Expressions.
            var regex = new Array();
            regex.push("[A-Z]"); //Uppercase Alphabet.
            regex.push("[a-z]"); //Lowercase Alphabet.
            regex.push("[0-9]"); //Digit.
            regex.push("[$@$!%*#?&]"); //Special Character.

            var passed = 0;

            //Validate for each Regular Expression.
            for (var i = 0; i < regex.length; i++) {
                if (new RegExp(regex[i]).test(password)) {
                    passed++;
                }
            }

            switch (passed) {
                case 0:
                case 1:
                case 2:
                    password_strength.removeClass('btn-warning btn-success').addClass('btn-danger').css('width', '30%');
                    break;
                case 3:
                    password_strength.removeClass('btn-danger btn-success').addClass('btn-warning').css('width', '60%');
                    break;
                case 4:
                    password_strength.removeClass('btn-danger btn-warning').addClass('btn-success').css('width', '100%');
                    break;

            }

        },


        save: function (e) {
            e.preventDefault();
            var targetUrl = $(".change-pass-form").attr("action");

            /*ولیدیت کردن فرم*/
            var $form = $("form");
            $form.validate();
            if (!$form.valid())
                return false;

            var captcha = $('#captcha').val();
            if (!captcha) {
                showNotification('کد امنیتی را وارد کنید!', 'danger');
                return false;
            }

            /*جمع آوری دیتای فرم*/
            var data = $(".change-pass-form").serialize();

            /*ثبت فرم با اجکس*/
            $.post(targetUrl,
                data,
                function (res) {
                    changeCaptcha();
                    if (res.status) {
                        $('.change-password-aler').remove();
                        $('#OldPassword').val('');
                        $('#Password').val('');
                        $('#RePassword').val('');
                        swal({
                            title: 'تغییر یافت.',
                            text: 'کلمه عبور با موفقیت تغییر یافت.',
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            confirmButtonText: "باشه",
                            buttonsStyling: false
                        }).then(function (isConfirm) {
                            if (res.model = true)
                                window.location.href = "/Admin/Dashboard";
                        });

                        if (res.model = true) {
                            setTimeout(() => {
                                window.location.href = "/Admin/Dashboard";
                            }, 3000);
                        }
                    }
                    else {
                        showNotification(res.message, 'danger');
                    }
                }).fail(function () {
                    changeCaptcha();
                    showNotification('تغییر کلمه عبور با خطا همراه بوده است!', 'danger');
                });
            return false;
        }
    },




    // تغییر کلمه عبور پرسنل
    changePassword: {
        // لود کردن فرم تغییر کلمه عبور
        loadForm: function (id, fullName) {
            $.get("/AuthSystem/users/LoadChangePasswordForm/" + id,
                { FullName: fullName },
                function (res) {
                    $("#modal-form").html(res);
                    $('#base-modal').addClass('small-modal');
                    modal.open();

                    /*اعمال ولیدیشن به فرمی که با اجکس لود شده است*/
                    var form = $(".change-pass-form")
                        .removeData("validator")
                        .removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse(form);
                });
        },



        // ذخیره کلمه عبور جدید
        save: function (e) {
            if (startSubmition == true)
                return false;
            startSubmition = true;

            e.preventDefault();
            var targetUrl = $(".change-pass-form").attr("action");

            /*ولیدیت کردن فرم*/
            var $form = $("form");
            $form.validate();
            if (!$form.valid()) {
                startSubmition = false;
                return false;
            }

            /*جمع آوری دیتای فرم*/
            var data = $(".change-pass-form").serialize();

            /*ثبت فرم با اجکس*/
            $.post(targetUrl,
                data,
                function (res) {
                    startSubmition = false;
                    if (res.status) {
                        modal.close();
                        swal({
                            title: 'تغییر یافت.',
                            text: 'کلمه عبور با موفقیت تغییر یافت.',
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            confirmButtonText: "باشه",
                            buttonsStyling: false
                        });
                    }
                    else {
                        $('.change-pass-form .error').html(res.message);
                    }
                }).fail(function () {
                    startSubmition = false;
                    $('.change-pass-form .error').html('تغییر کلمه عبور با خطا همراه بوده است!');
                });
            return false;
        }
    },





    // ریست کردن کلمه عبور پرسنل
    resetPassword: {
        loadForm: function (id) {
            if (!id)
                showNotification("لطفا ابتدا پرسنل مورد نظر را انتخاب نمایید !");
            else
                swal({
                    title: 'آیا مطمئنید ؟',
                    text: "کلمه عبور کاربر ریست می شود و کلمه عبور جدید به تلفن همراه ثبت شده برای کاربر پیامک می گردد.",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonClass: 'btn btn-danger',
                    cancelButtonClass: 'btn btn-default',
                    confirmButtonText: 'بله ، ریست شود !',
                    cancelButtonText: 'لغو',
                    buttonsStyling: false
                }).then(function (isConfirm) {
                    if (isConfirm)
                        user.resetPassword.confirm(id);
                });
        },

        confirm: function (id) {
            $.post("/AuthSystem/users/resetPassword/" + id,
                function (res) {
                    if (res.status) {
                        user.list.reload();
                        swal({
                            title: 'ریست شد !',
                            text: 'کلمه عبور پرسنل مورد نظر با موفقیت ریست شد.',
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            confirmButtonText: "باشه",
                            buttonsStyling: false
                        });
                    }
                    else {
                        swal({
                            title: 'ریست نشد !',
                            text: res.message,
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            confirmButtonText: "باشه",
                            buttonsStyling: false
                        });
                    }
                }).fail(function () {
                    swal({
                        title: 'ریست نشد !',
                        text: 'ریست کلمه عبور پرسنل با خطا همراه بوده است. مجددا اقدام کنید!',
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        confirmButtonText: "باشه",
                        buttonsStyling: false
                    });
                });

        }
    },



    // حذف پرسنل
    delete: {
        loadForm: function (id) {
            if (!id)
                showNotification("لطفا ابتدا پرسنل مورد نظر را انتخاب نمایید !");
            else
                swal({
                    title: 'آیا مطمئنید ؟',
                    text: "بعد از حذف، اطلاعات پرسنل قابل برگشت نیست!",
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonClass: 'btn btn-danger',
                    cancelButtonClass: 'btn btn-default',
                    confirmButtonText: 'بله ، حذف شود !',
                    cancelButtonText: 'لغو',
                    buttonsStyling: false
                }).then(function (isConfirm) {
                    if (isConfirm)
                        user.delete.confirm(id);
                });
        },

        confirm: function (id) {
            $.post("/AuthSystem/users/Delete/" + id,
                function (res) {
                    if (res.status) {
                        user.list.reload();
                        swal({
                            title: 'حذف شد !',
                            text: 'پرسنل مورد نظر با موفقیت حذف شد',
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            confirmButtonText: "باشه",
                            buttonsStyling: false
                        });
                    }
                    else {
                        swal({
                            title: 'حذف نشد !',
                            text: res.message,
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            confirmButtonText: "باشه",
                            buttonsStyling: false
                        });
                    }
                }).fail(function () {
                    swal({
                        title: 'حذف نشد !',
                        text: 'حذف پرسنل با خطا همراه بوده است. مجددا اقدام کنید!',
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        confirmButtonText: "باشه",
                        buttonsStyling: false
                    });
                });

        }

    },



    //تغییر وضعیت فعال بودن یا نبودن کاربر در صفحه ایندکس
    toggleEnable: function (el, id) {
        $.post('/AuthSystem/users/ToggleEnable/' + id, function (res) {
            if (!res.status)
                alert('تغییر وضعیت کاربر با خطا همراه بوده است!');
            //if (res.isEnabled == true)
            //    $(el).removeClass('btn-danger').addClass('btn-success').text('فعال');
            //else
            //    $(el).removeClass('btn-success').addClass('btn-danger').text('غیرفعال');
        })
    }

}





//==============================================
// فیلتر جستجو درخواست ها
//==============================================
var filter = {
    initial: _ => {
        filter.startDate.initial();
        filter.endDate.initial();
        filter.role.initial();

        $('.selectpicker').selectpicker('refresh');
    },



    //جمع آوری داده فیلتر
    collect: _ => {
        var data = {
            StartDate: $("input[name=FilterStartDate]").val(),
            EndDate: $("input[name=FilterEndDate]").val(),
            Name: $("#FilterName").val(),
            Username: $("#FilterUsername").val(),
            Mobile: $("#FilterMobile").val(),
            RoleId: $("#FilterRoleId").val(),
            IsEnabled: $("#FilterIsEnabled").val(),
        }

        return data;
    },


    // تاریخ شروع
    startDate: {

        // آبجکت vue
        obj: null,

        //راه اندازی تقویم تاریخ شروع
        initial: function () {
            if ($('#VueStartDate').length == 0)
                return;
            var sdate = ''; //moment()/*.subtract(7, 'd')*/.format('jYYYY/jMM/jDD');
            this.obj = new Vue({
                el: '#VueStartDate',
                data: {
                    date: sdate
                },
                components: {
                    DatePicker: VuePersianDatetimePicker
                },
                methods: {
                    onClose: function (x) {
                        filter.checkDates(filter.startDate, filter.endDate);
                    }
                }
            });
        }
    },



    // تاریخ پایان
    endDate: {
        // آبجکت vue
        obj: null,

        //راه اندازی تقویم تاریخ پایان
        initial: function () {
            if ($('#VueEndDate').length == 0)
                return;
            var edate = ''; // moment().format('jYYYY/jMM/jDD');
            this.obj = new Vue({
                el: '#VueEndDate',
                data: {
                    date: edate
                },
                components: {
                    DatePicker: VuePersianDatetimePicker
                },
                methods: {
                    onClose: function (x) {
                        filter.checkDates(filter.startDate, filter.endDate);
                    }
                }
            });
        }
    },



    // بررسی تاریخ شروع و پایان
    checkDates: function (start, end) {
        var sdate = start.obj.date;
        var edate = end.obj.date;
        if (sdate && edate && sdate > edate) {
            showNotification("تاریخ شروع نمیتواند بعد از تاریخ پایان باشد!", 'danger');
            $("input[name=FilterStartDate]").val('').prev('input').val('');
            $("input[name=FilterEndDate]").val('').prev('input').val('');
        }
    },



    //نقش ها
    role: {
        initial: _ => {
            if ($('#FilterRoleId').length == 0)
                return;

            var opt = "<option value='' selected>همه</option>"
            $('#FilterRoleId').prepend(opt);
        }
    },



    // خالی کردن سلکتایز از ایتم انتخاب شده
    clearSelectize: function (el) {
        var $select = $(el).selectize();
        var control = $select[0].selectize;
        control.clear();
        control.renderCache = {};
        control.clearOptions();
        control.refreshOptions(true);
    },



}





if (controller == 'profile' && action == 'edit')
    breadcrumb.push({ title: "ویرایش پروفایل", link: "#" });
else if (controller == 'profile' && action == 'changepassword')
    breadcrumb.push({ title: "تغییر کلمه عبور", link: "#" });
else {
    breadcrumb.push({ title: "کاربران", link: "#" });
    user.list.initial();
    filter.initial();
}


// تغییر تصویر کپچا
var changeCaptcha = () => {
    var d = new Date();
    $("#imgcpatcha").attr("src", "/Captcha/CaptchaImage?" + d.getTime());
    $('#captcha').val('');
}