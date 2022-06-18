
if (!window.singleFormHelpers) {
    window.singleFormHelpers = {};
}

if (!window.singleFormHelpers.DBD) {
    window.singleFormHelpers.DBD = {

        checkTransferCommerceById: function (sectionGroup, commerceNo, registerNo, btnCheck, callback) {

            if (!commerceNo || !registerNo) {
                swal('', "กรุณาระบุเลขทะเบียนพาณิชย์ที่โอนมา และ เลขที่คำขอจัดตั้งที่โอนมา", "warning");
                return;
            }

            var l = $(btnCheck).ladda();
            l.ladda('start');

            $.ajax({
                url: window.sharedConfig.baseUrl + '/Api/v2/DBD/CheckTransferCommerceById',
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    "CommerceNo": commerceNo,
                    "RegisterNo": registerNo,
                    "SectionGroup": sectionGroup
                }),
                timeout: 30000,
                success: function (data) {
                    $.ladda('stopAll');
                    if (data.Success) {
                        if (callback) callback(data);
                    } else {
                        swal('', "ไม่พบข้อมูลการโอนทะเบียนพาณิชย์", "warning");
                    }
                },
                error: function (err) {
                    console.log('error', err);
                    $.ladda('stopAll');
                }
            });
        },

        checkCommerceById: function (sectionGroup, commerceNo, registerNo, btnCheck, callback) {

            if (!commerceNo || !registerNo) {
                swal('', "กรุณาระบุเลขทะเบียนพาณิชย์ และเลขที่คำขอจัดตั้ง", "warning");
                return;
            }

            var l = $(btnCheck).ladda();
            l.ladda('start');

            $.ajax({
                url: window.sharedConfig.baseUrl + '/Api/v2/DBD/CheckCommerceById',
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    "CommerceNo": commerceNo,
                    "RegisterNo": registerNo,
                    "SectionGroup": sectionGroup
                }),
                timeout: 30000,
                success: function (data) {
                    $.ladda('stopAll');
                    if (data.Success) {
                        if (callback) callback(data);
                    } else {
                        swal('', "ไม่พบข้อมูลการจดทะเบียนพาณิชย์ หรือพาณิชย์อิเล็กทรอนิกส์", "warning");
                    }
                },
                error: function (err) {
                    console.log('error', err);
                    swal('', "เกิดความผิดพลาดขณะเชื่อมต่อกับกรมพัฒนาธุรกิจการค้า", "error");
                    $.ladda('stopAll');
                }
            });
        },

        getCommerceList: function (sectionGroup, identityId, registerNo, btnCheck, callback) {

            if (!identityId || !registerNo) {
                swal('', "กรุณาระบุเลขทะเบียนนิติบุคคล/เลขที่บัตรประชาชน และเลขที่คำขอจัดตั้ง", "warning");
                return;
            }

            var l = $(btnCheck).ladda();
            l.ladda('start');

            $.ajax({
                url: window.sharedConfig.baseUrl + '/Api/v2/DBD/GetCommerceList',
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    "IdentityID": identityId,
                    "RegisterNo": registerNo,
                    "SectionGroup": sectionGroup
                }),
                timeout: 30000,
                success: function (data) {
                    $.ladda('stopAll');

                    if (callback) callback(data);
                },
                error: function (err) {
                    console.log('error', err);
                    $.ladda('stopAll');
                }
            });
        }
    };
}



