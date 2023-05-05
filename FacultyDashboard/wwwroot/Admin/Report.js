function Education_1LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد دانشجویان شاغل به تحصیل";
    $.ajax({
        type: "GET",
        url: '/Education/Education_1/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var m_dataList = data.maldata;
            var f_dataList = data.femdata;
            var categories = data.cate;
            var flst = [], mlst = [];
            var dataResult_1 = [];
            if (m_dataList != null || f_dataList != null) {
                for (var i = 0; i < m_dataList.length; i++) {
                    mlst.push(parseInt(m_dataList[i].col2));
                }
                for (var i = 0; i < f_dataList.length; i++) {
                    flst.push(parseInt(f_dataList[i].col3));
                }
                var mal_data = {
                    name: 'مرد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
                var femal_data = {
                    name: 'زن',
                    data: flst,
                    stack: 'female'
                };
                dataResult_1.push(femal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}
function Education_10LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "ميانگين معدل سالانه دانشجويان";
    $.ajax({
        type: "GET",
        url: '/Education/Education_10/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var categories = data.cate;
            var mlst = [];
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    mlst.push(parseFloat(kol_dataList[i].col2));
                }
                var mal_data = {
                    name: 'میانگین معدل',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}
function Education_11LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد دانشجویان المپیاد ";
    $.ajax({
        type: "GET",
        url: '/Education/Education_11/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var m_dataList = data.maldata;
            var f_dataList = data.femdata;
            var categories = data.cate;
            var flst = [], mlst = [];
            var dataResult_1 = [];
            if (m_dataList != null || f_dataList != null) {
                for (var i = 0; i < m_dataList.length; i++) {
                    mlst.push(parseInt(m_dataList[i].col2));
                }
                for (var i = 0; i < f_dataList.length; i++) {
                    flst.push(parseInt(f_dataList[i].col3));
                }
                var mal_data = {
                    name: 'مرد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
                var femal_data = {
                    name: 'زن',
                    data: flst,
                    stack: 'female'
                };
                dataResult_1.push(femal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}
function Education_12LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد دانشجویان استعداد درخشان";
    $.ajax({
        type: "GET",
        url: '/Education/Education_12/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var m_dataList = data.maldata;
            var f_dataList = data.femdata;
            var categories = data.cate;
            var flst = [], mlst = [];
            var dataResult_1 = [];
            if (m_dataList != null || f_dataList != null) {
                for (var i = 0; i < m_dataList.length; i++) {
                    mlst.push(parseInt(m_dataList[i].col2));
                }
                for (var i = 0; i < f_dataList.length; i++) {
                    flst.push(parseInt(f_dataList[i].col3));
                }
                var mal_data = {
                    name: 'مرد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
                var femal_data = {
                    name: 'زن',
                    data: flst,
                    stack: 'female'
                };
                dataResult_1.push(femal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}
function Education_2LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد دانشجویان ورودی";
    $.ajax({
        type: "GET",
        url: '/Education/Education_2/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var m_dataList = data.maldata;
            var f_dataList = data.femdata;
            var categories = data.cate;
            var flst = [], mlst = [];
            var dataResult_1 = [];
            if (m_dataList != null || f_dataList != null) {
                for (var i = 0; i < m_dataList.length; i++) {
                    mlst.push(parseInt(m_dataList[i].col2));
                }
                for (var i = 0; i < f_dataList.length; i++) {
                    flst.push(parseInt(f_dataList[i].col3));
                }
                var mal_data = {
                    name: 'مرد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
                var femal_data = {
                    name: 'زن',
                    data: flst,
                    stack: 'female'
                };
                dataResult_1.push(femal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}
function Education_3LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "آمار دانشجویان بورسیه ( سپاه،ناجا،وزارت دفاع)";
    $.ajax({
        type: "GET",
        url: '/Education/Education_3/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var m_dataList = data.maldata;
            var f_dataList = data.femdata;
            var categories = data.cate;
            var flst = [], mlst = [];
            var dataResult_1 = [];
            if (m_dataList != null || f_dataList != null) {
                for (var i = 0; i < m_dataList.length; i++) {
                    mlst.push(parseInt(m_dataList[i].col2));
                }
                for (var i = 0; i < f_dataList.length; i++) {
                    flst.push(parseInt(f_dataList[i].col3));
                }
                var mal_data = {
                    name: 'مرد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
                var femal_data = {
                    name: 'زن',
                    data: flst,
                    stack: 'female'
                };
                dataResult_1.push(femal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}
function Education_4LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "آمار دانشجویان آزاد";
    $.ajax({
        type: "GET",
        url: '/Education/Education_4/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var m_dataList = data.maldata;
            var f_dataList = data.femdata;
            var categories = data.cate;
            var flst = [], mlst = [];
            var dataResult_1 = [];
            if (m_dataList != null || f_dataList != null) {
                for (var i = 0; i < m_dataList.length; i++) {
                    mlst.push(parseInt(m_dataList[i].col2));
                }
                for (var i = 0; i < f_dataList.length; i++) {
                    flst.push(parseInt(f_dataList[i].col3));
                }
                var mal_data = {
                    name: 'مرد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
                var femal_data = {
                    name: 'زن',
                    data: flst,
                    stack: 'female'
                };
                dataResult_1.push(femal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}
function Education_5LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "آمار فارغ‌التحصیلان به تفکیک مقطع تحصیلی و جنسیت";
    $.ajax({
        type: "GET",
        url: '/Education/Education_5/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var m_dataList = data.maldata;
            var f_dataList = data.femdata;
            var categories = data.cate;
            var flst = [], mlst = [];
            var dataResult_1 = [];
            if (m_dataList != null || f_dataList != null) {
                for (var i = 0; i < m_dataList.length; i++) {
                    mlst.push(parseInt(m_dataList[i].col2));
                }
                for (var i = 0; i < f_dataList.length; i++) {
                    flst.push(parseInt(f_dataList[i].col3));
                }
                var mal_data = {
                    name: 'مرد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
                var femal_data = {
                    name: 'زن',
                    data: flst,
                    stack: 'female'
                };
                dataResult_1.push(femal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}
function Education_6LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "نسبت دانشجویان به اساتید";
    $.ajax({
        type: "GET",
        url: '/Education/Education_6/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    var newdata = {
                        name: kol_dataList[i].col1,
                        y: parseFloat(kol_dataList[i].col2),
                        count: kol_dataList[i].col2
                    }
                    dataResult_1.push(newdata);
                }
            }
            var fi_data = {
                type: 'pie',
                name: 'Browser share',
                data: dataResult_1
            };
            drawPieChart(fi_data, chartcontainerName, title);
        }
    })
}
function Education_7LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "دانشجویان مشروط(معدل زیر 12) ";
    $.ajax({
        type: "GET",
        url: '/Education/Education_7/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var m_dataList = data.maldata;
            var f_dataList = data.femdata;
            var categories = data.cate;
            var flst = [], mlst = [];
            var dataResult_1 = [];
            if (m_dataList != null || f_dataList != null) {
                for (var i = 0; i < m_dataList.length; i++) {
                    mlst.push(parseInt(m_dataList[i].col2));
                }
                for (var i = 0; i < f_dataList.length; i++) {
                    flst.push(parseInt(f_dataList[i].col3));
                }
                var mal_data = {
                    name: 'مرد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
                var femal_data = {
                    name: 'زن',
                    data: flst,
                    stack: 'female'
                };
                dataResult_1.push(femal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}
function Education_8LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "دانشجویان مهمان از دانشکده پیراپزشکی به سایر دانشکده ها...";
    $.ajax({
        type: "GET",
        url: '/Education/Education_8/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var m_dataList = data.maldata;
            var f_dataList = data.femdata;
            var categories = data.cate;
            var flst = [], mlst = [];
            var dataResult_1 = [];
            if (m_dataList != null || f_dataList != null) {
                for (var i = 0; i < m_dataList.length; i++) {
                    mlst.push(parseInt(m_dataList[i].col2));
                }
                for (var i = 0; i < f_dataList.length; i++) {
                    flst.push(parseInt(f_dataList[i].col3));
                }
                var mal_data = {
                    name: 'مرد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
                var femal_data = {
                    name: 'زن',
                    data: flst,
                    stack: 'female'
                };
                dataResult_1.push(femal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}
function Education_9LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "آمار دانشجویان مهمان از سایر دانشکده های دانشگاه ع پ ارتش به دانشکده پیراپزشکی";
    $.ajax({
        type: "GET",
        url: '/Education/Education_9/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var m_dataList = data.maldata;
            var f_dataList = data.femdata;
            var categories = data.cate;
            var flst = [], mlst = [];
            var dataResult_1 = [];
            if (m_dataList != null || f_dataList != null) {
                for (var i = 0; i < m_dataList.length; i++) {
                    mlst.push(parseInt(m_dataList[i].col2));
                }
                for (var i = 0; i < f_dataList.length; i++) {
                    flst.push(parseInt(f_dataList[i].col3));
                }
                var mal_data = {
                    name: 'مرد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
                var femal_data = {
                    name: 'زن',
                    data: flst,
                    stack: 'female'
                };
                dataResult_1.push(femal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}

function IT_1LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد قراردادهای پژوهشی منعقد شده دانشکده با صنعت";
    $.ajax({
        type: "GET",
        url: '/IT/IT_1/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var kol = data.kol;
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    var newdata = {
                        name: kol_dataList[i].col1,
                        y: parseFloat(kol_dataList[i].col2),
                        count: kol_dataList[i].col2
                    }
                    dataResult_1.push(newdata);
                }
            }
            var fi_data = {
                type: 'pie',
                name: 'Browser share',
                data: dataResult_1
            };
            drawPieChart(fi_data, chartcontainerName, title+"  "+ kol);
        }
    })
}
function IT_2LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد شرکت های دانش بنیان ایجاد شده توسط استادان و دانشجویان دانشکده ثبت اختراعات(کلی و به تفکیک گروه آموزشی";
    $.ajax({
        type: "GET",
        url: '/IT/IT_2/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var kol = data.kol;
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    var newdata = {
                        name: kol_dataList[i].col1,
                        y: parseFloat(kol_dataList[i].col2),
                        count: kol_dataList[i].col2
                    }
                    dataResult_1.push(newdata);
                }
            }
            var fi_data = {
                type: 'pie',
                name: 'Browser share',
                data: dataResult_1
            };
            drawPieChart(fi_data, chartcontainerName, title + "  " + kol);
        }
    })
}
function IT_3LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تجاری سازی نتایج تحقیقات و پژوهش های دانشکده (کلی و به تفکیک گروه آموزشی)";
    $.ajax({
        type: "GET",
        url: '/IT/IT_3/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var kol = data.kol;
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    var newdata = {
                        name: kol_dataList[i].col1,
                        y: parseFloat(kol_dataList[i].col2),
                        count: kol_dataList[i].col2
                    }
                    dataResult_1.push(newdata);
                }
            }
            var fi_data = {
                type: 'pie',
                name: 'Browser share',
                data: dataResult_1
            };
            drawPieChart(fi_data, chartcontainerName, title + "  " + kol);
        }
    })
}
function IT_4LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد ثبت اختراعات به تفکیک گروه آموزشی";
    $.ajax({
        type: "GET",
        url: '/IT/IT_4/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var kol = data.kol;
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    var newdata = {
                        name: kol_dataList[i].col1,
                        y: parseFloat(kol_dataList[i].col2),
                        count: kol_dataList[i].col2
                    }
                    dataResult_1.push(newdata);
                }
            }
            var fi_data = {
                type: 'pie',
                name: 'Browser share',
                data: dataResult_1
            };
            drawPieChart(fi_data, chartcontainerName, title + "  " + kol);
        }
    })
}
function IT_5LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "کارگاه آموزشی";
    $.ajax({
        type: "GET",
        url: '/IT/IT_5/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            if (kol_dataList != null) {
                $("#chrow").empty();
                var ti = $('<h4>' + title + '</h4><br/>');
                var table = $('<table class="table table-striped table-hover"></table>');//.addClass('foo');
                var baseHtml = "";
                if (kol_dataList.length > 0) {
                    var header_rows = "", data_rows = "";
                    header_rows += "<th>عنوان کارگاه</th><th>گروه آموزشی</th><th>تاریخ برگزاری</th>"
                    baseHtml = ('<tr>' + header_rows + '</tr>');
                    for (let i = 0; i < kol_dataList.length; i++) {
                        data_rows += ('<tr>');
                        data_rows += ('<td width="100px">' + kol_dataList[i].col1 + '</td>');
                        data_rows += ('<td width="100px">' + kol_dataList[i].col2 + '</td>');
                        data_rows += ('<td width="100px">' + kol_dataList[i].col3 + '</td>');
                        data_rows += ('</tr>');
                    }
                    baseHtml += data_rows;
                }
                table.append(baseHtml);
                $("#chrow").append(ti);
                $("#chrow").append(table);
            }
        }
    })
}
function IT_6LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "کارگاه های مهارت آموزی( مختص کارکنانی که دریافت امتیاز آموزش مداون برای آنها مطرح نیست";
    $.ajax({
        type: "GET",
        url: '/IT/IT_6/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            if (kol_dataList != null) {
                $("#chrow").empty();
                var ti = $('<h4>' + title + '</h4><br/>');
                var table = $('<table class="table table-striped table-hover"></table>');//.addClass('foo');
                var baseHtml = "";
                if (kol_dataList.length > 0) {
                    var header_rows = "", data_rows = "";
                    header_rows += "<th>عنوان کارگاه</th><th>گروه آموزشی</th><th>تاریخ برگزاری</th>"
                    baseHtml = ('<tr>' + header_rows + '</tr>');
                    for (let i = 0; i < kol_dataList.length; i++) {
                        data_rows += ('<tr>');
                        data_rows += ('<td width="100px">' + kol_dataList[i].col1 + '</td>');
                        data_rows += ('<td width="100px">' + kol_dataList[i].col2 + '</td>');
                        data_rows += ('<td width="100px">' + kol_dataList[i].col3 + '</td>');
                        data_rows += ('</tr>');
                    }
                    baseHtml += data_rows;
                }
                table.append(baseHtml);
                $("#chrow").append(ti);
                $("#chrow").append(table);
            }
        }
    })
}
function IT_7LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "برگزاری دوره های مهارت آموزی ( تدوین اقدامات پیش بیمارستانی)";
    $.ajax({
        type: "GET",
        url: '/IT/IT_7/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            if (kol_dataList != null) {
                $("#chrow").empty();
                var ti = $('<h4>' + title + '</h4><br/>');
                var table = $('<table class="table table-striped table-hover"></table>');//.addClass('foo');
                var baseHtml = "";
                if (kol_dataList.length > 0) {
                    var header_rows = "", data_rows = "";
                    header_rows += "<th>عنوان کارگاه</th><th>گروه آموزشی</th><th>تاریخ برگزاری</th>"
                    baseHtml = ('<tr>' + header_rows + '</tr>');
                    for (let i = 0; i < kol_dataList.length; i++) {
                        data_rows += ('<tr>');
                        data_rows += ('<td width="100px">' + kol_dataList[i].col1 + '</td>');
                        data_rows += ('<td width="100px">' + kol_dataList[i].col2 + '</td>');
                        data_rows += ('<td width="100px">' + kol_dataList[i].col3 + '</td>');
                        data_rows += ('</tr>');
                    }
                    baseHtml += data_rows;
                }
                table.append(baseHtml);
                $("#chrow").append(ti);
                $("#chrow").append(table);
            }
        }
    })
}

function Research_1LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد مقاله های نمایه شده در مجلات";
    $.ajax({
        type: "GET",
        url: '/Research/Research_1/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var categories = data.cate;
            var mlst = [];
            var dataResult_1 = [];
            if (kol_dataList != null) {
                var mal_data1 = {
                    name: "pubmed",
                    //name: "گروه علوم آزمایشگاهی",
                    data: [parseInt(kol_dataList[0].col3), parseInt(kol_dataList[1].col3), parseInt(kol_dataList[2].col3), parseInt(kol_dataList[3].col3), parseInt(kol_dataList[4].col3), parseInt(kol_dataList[5].col3), parseInt(kol_dataList[6].col3)],//mlst,
                    //stack: 'Asia'
                };
                dataResult_1.push(mal_data1);
                var mal_data2 = {
                    name: "scopus",
                    //name: "گروه رادیولوژی",
                    data: [parseInt(kol_dataList[0].col4), parseInt(kol_dataList[1].col4), parseInt(kol_dataList[2].col4), parseInt(kol_dataList[3].col4), parseInt(kol_dataList[4].col4), parseInt(kol_dataList[5].col4), parseInt(kol_dataList[6].col4)],//mlst,
                    //stack: 'Asia'
                };
                dataResult_1.push(mal_data2);
                var mal_data3 = {
                    name: "ISI",
                    //name: "گروه هوشبری",
                    data: [parseInt(kol_dataList[0].col5), parseInt(kol_dataList[1].col5), parseInt(kol_dataList[2].col5), parseInt(kol_dataList[3].col5), parseInt(kol_dataList[4].col5), parseInt(kol_dataList[5].col5), parseInt(kol_dataList[6].col5)],//mlst,
                    //stack: 'Asia'
                };
                dataResult_1.push(mal_data3);
                var mal_data4 = {
                    name: "علمی - پژوهشی",
                    //name: "گروه فناوری اطلاعات سلامت",
                    data: [parseInt(kol_dataList[0].col6), parseInt(kol_dataList[1].col6), parseInt(kol_dataList[2].col6), parseInt(kol_dataList[3].col6), parseInt(kol_dataList[4].col6), parseInt(kol_dataList[5].col6), parseInt(kol_dataList[6].col6)],//mlst,
                    //stack: 'Asia'
                };
                dataResult_1.push(mal_data4);
                var mal_data5 = {
                    name: "علمی - ترویجی",
                    //name: "گروه اتاق عمل",
                    data: [parseInt(kol_dataList[0].col7), parseInt(kol_dataList[1].col7), parseInt(kol_dataList[2].col7), parseInt(kol_dataList[3].col7), parseInt(kol_dataList[4].col7), parseInt(kol_dataList[5].col7), parseInt(kol_dataList[6].col7)],//mlst,
                    //stack: 'Asia'
                };
                dataResult_1.push(mal_data5);
                var mal_data6 = {
                    name: "کنفرانس های داخلی",
                    //name: "گروه فوریتهای پزشکی",
                    data: [parseInt(kol_dataList[0].col8), parseInt(kol_dataList[1].col8), parseInt(kol_dataList[2].col8), parseInt(kol_dataList[3].col8), parseInt(kol_dataList[4].col8), parseInt(kol_dataList[5].col8), parseInt(kol_dataList[6].col8)],//mlst,
                    //stack: 'Asia'
                };
                dataResult_1.push(mal_data6);
                var mal_data7 = {
                    name: "کنفرانس های بین المللی",
                    //name: "گروه زبان",
                    data: [parseInt(kol_dataList[0].col9), parseInt(kol_dataList[1].col9), parseInt(kol_dataList[2].col9), parseInt(kol_dataList[3].col9), parseInt(kol_dataList[4].col9), parseInt(kol_dataList[5].col9), parseInt(kol_dataList[6].col9)],//mlst,
                    //stack: 'Asia'
                };
                dataResult_1.push(mal_data7);
                //debugger;
                drawColumnChart_3("", dataResult_1, categories, chartcontainerName, title, "");
            }
        }
    })
}
function Research_2LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد سمینارهای علمی داخلی و بین المللی برگزار شده در دانشکده";
    $.ajax({
        type: "GET",
        url: '/Research/Research_2/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var ti = $('<h4>' + title + '</h4><br/>');
            if (kol_dataList != null) {
                $("#chrow").empty();
                var baseHtml = "";
                if (kol_dataList.length > 0) {
                    for (let i = 0; i < kol_dataList.length; i++) {
                        baseHtml += ('<div class="col-md-6 col-xl-3"><div class="card" id="tooltip-container"><div class="card-body">' +
                            '<h4 class="mt-0 font-16">' + kol_dataList[i].col1 + '</h4><h2 class="text-primary my-3 text-center">' +
                            '<span data-plugin="counterup">' + kol_dataList[i].col2 + '</span></h2></div></div></div>');
                    }
                }
                $("#chrow").append(ti);
                $("#chrow").append(baseHtml);
            }
        }
    })
}
function Research_3LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد مقالات مشترک با محققان دیگر کشورها(کلی و به تفکیک گروه آموزشی)";
    $.ajax({
        type: "GET",
        url: '/Research/Research_3/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var ti = $('<h4>' + title + '</h4><br/>');
            if (kol_dataList != null) {
                $("#chrow").empty();
                var baseHtml = "";
                if (kol_dataList.length > 0) {
                    for (let i = 0; i < kol_dataList.length; i++) {
                        baseHtml += ('<div class="col-md-6 col-xl-3"><div class="card" id="tooltip-container"><div class="card-body">' +
                            '<h4 class="mt-0 font-16">' + kol_dataList[i].col1 + '</h4><h2 class="text-primary my-3 text-center">' +
                            '<span data-plugin="counterup">' + kol_dataList[i].col2 + '</span></h2></div></div></div>');
                    }
                }
                $("#chrow").append(ti);
                $("#chrow").append(baseHtml);
            }
        }
    })
}
function Research_4LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد طرح‌های انجام شده و در حال انجام  برحسب سال";
    $.ajax({
        type: "GET",
        url: '/Research/Research_4/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            if (kol_dataList != null) {
                $("#chrow").empty();
                var ti = $('<h4>' + title + '</h4><br/>');
                var baseHtml = "";
                if (kol_dataList.length > 0) {
                    for (let i = 0; i < kol_dataList.length; i++) {
                        baseHtml += ('<div class="col-md-6 col-xl-3"><div class="card" id="tooltip-container"><div class="card-body">' +
                            '<h4 class="mt-0 font-16">' + kol_dataList[i].col1 + '</h4><h2 class="text-primary my-3 text-center">' +
                            '<span data-plugin="counterup">' + kol_dataList[i].col2 + '</span></h2></div></div></div>');
                    }
                }
                $("#chrow").append(ti);
                $("#chrow").append(baseHtml);
            }
        }
    })
}
function Research_5LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد پایان نامه های مقاطع تحصیلی کارشناسی ارشد و دکتری در حال انجام";
    $.ajax({
        type: "GET",
        url: '/Research/Research_5/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            if (kol_dataList != null) {
                var ti = $('<h4>' + title + '</h4><br/>');
                $("#chrow").empty();
                var baseHtml = "";
                if (kol_dataList.length > 0) {
                    for (let i = 0; i < kol_dataList.length; i++) {
                        baseHtml += ('<div class="col-md-6 col-xl-3"><div class="card" id="tooltip-container"><div class="card-body">' +
                            '<h4 class="mt-0 font-16">' + kol_dataList[i].col1 + '</h4><h2 class="text-primary my-3 text-center">' +
                            '<span data-plugin="counterup">' + kol_dataList[i].col2 + '</span></h2></div></div></div>');
                    }
                }
                $("#chrow").append(ti);
                $("#chrow").append(baseHtml);
            }
        }
    })
}
function Research_6LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد پایان نامه های مقاطع تحصیلی کارشناسی ارشد و دکتری انجام شده";
    $.ajax({
        type: "GET",
        url: '/Research/Research_6/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            if (kol_dataList != null) {
                var ti = $('<h4>' + title + '</h4><br/>');
                $("#chrow").empty();
                var baseHtml = "";
                if (kol_dataList.length > 0) {
                    for (let i = 0; i < kol_dataList.length; i++) {
                        baseHtml += ('<div class="col-md-6 col-xl-3"><div class="card" id="tooltip-container"><div class="card-body">' +
                            '<h4 class="mt-0 font-16">' + kol_dataList[i].col1 + '</h4><h2 class="text-primary my-3 text-center">' +
                            '<span data-plugin="counterup">' + kol_dataList[i].col2 + '</span></h2></div></div></div>');
                    }
                }
                $("#chrow").append(ti);
                $("#chrow").append(baseHtml);
            }
        }
    })
}

function Resource_1LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد هیأت علمی";
    $.ajax({
        type: "GET",
        url: '/Resource/Resource_1/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    var newdata = {
                        name: kol_dataList[i].col1,
                        y: parseFloat(kol_dataList[i].col2),
                        count: kol_dataList[i].col2
                    }
                    dataResult_1.push(newdata);
                }
            }
            var fi_data = {
                type: 'pie',
                name: 'Browser share',
                data: dataResult_1
            };
            drawPieChart(fi_data, chartcontainerName, title);
        }
    })
}
function Resource_2LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد هیأت علمی به تفکیک گروه آموزشی";
    $.ajax({
        type: "GET",
        url: '/Resource/Resource_2/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var categories = data.cate;
            var mlst = [];
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    mlst.push(parseInt(kol_dataList[i].col2));
                }
                var mal_data = {
                    name: 'تعداد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}
function Resource_3LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد هیأت علمی به تفکیک گروه آموزشی";
    $.ajax({
        type: "GET",
        url: '/Resource/Resource_3/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var categories = data.cate;
            var mlst = [];
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    mlst.push(parseInt(kol_dataList[i].col2));
                }
                var mal_data = {
                    name: 'تعداد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}
function Resource_4LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد کارکنان اداری";
    $.ajax({
        type: "GET",
        url: '/Resource/Resource_4/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    var newdata = {
                        name: kol_dataList[i].col1,
                        y: parseFloat(kol_dataList[i].col2),
                        count: kol_dataList[i].col2
                    }
                    dataResult_1.push(newdata);
                }
            }
            var fi_data = {
                type: 'pie',
                name: 'Browser share',
                data: dataResult_1
            };
            drawPieChart(fi_data, chartcontainerName, title);
        }
    })
}
function Resource_5LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "نسبت كاركنان اداري به كل دانشجويان";
    $.ajax({
        type: "GET",
        url: '/Resource/Resource_5/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList_1 = data.koldata_1;
            var kol_dataList_2 = data.koldata_2;
            var dataResult_1 = [];
            if (kol_dataList_1 != null) {
                var newdata_1 = {
                    name: "كل دانشجويان",
                    y: parseFloat(kol_dataList_1),
                    count: kol_dataList_1
                }
                dataResult_1.push(newdata_1);
                var newdata_2 = {
                    name: "كاركنان اداري",
                    y: parseFloat(kol_dataList_2),
                    count: kol_dataList_2
                }
                dataResult_1.push(newdata_2);
            }
            var fi_data = {
                type: 'pie',
                name: 'Browser share',
                data: dataResult_1
            };
            drawPieChart(fi_data, chartcontainerName, title);
        }
    })
}
function Resource_6LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "نسبت كاركنان اداري به اعضاي هيأت علمي";
    $.ajax({
        type: "GET",
        url: '/Resource/Resource_6/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList_1 = data.koldata_1;
            var kol_dataList_2 = data.koldata_2;
            var dataResult_1 = [];
            if (kol_dataList_1 != null) {
                var newdata_1 = {
                    name: "اعضاي هيأت علمي",
                    y: parseFloat(kol_dataList_1),
                    count: kol_dataList_1
                }
                dataResult_1.push(newdata_1);
                var newdata_2 = {
                    name: "كاركنان اداري",
                    y: parseFloat(kol_dataList_2),
                    count: kol_dataList_2
                }
                dataResult_1.push(newdata_2);
            }
            var fi_data = {
                type: 'pie',
                name: 'Browser share',
                data: dataResult_1
            };
            drawPieChart(fi_data, chartcontainerName, title);
        }
    })
}
function Resource_7LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد هیأت علمی به مرتبه علمی و جنسیت";
    $.ajax({
        type: "GET",
        url: '/Resource/Resource_7/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var m_dataList = data.maldata;
            var f_dataList = data.femdata;
            var categories = data.cate;
            var flst = [], mlst = [];
            var dataResult_1 = [];
            if (m_dataList != null || f_dataList != null) {
                for (var i = 0; i < m_dataList.length; i++) {
                    mlst.push(parseInt(m_dataList[i].col2));
                }
                for (var i = 0; i < f_dataList.length; i++) {
                    flst.push(parseInt(f_dataList[i].col3));
                }
                var mal_data = {
                    name: 'مرد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
                var femal_data = {
                    name: 'زن',
                    data: flst,
                    stack: 'female'
                };
                dataResult_1.push(femal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}

function Student_1LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "اساتید مشاور";
    $.ajax({
        type: "GET",
        url: '/Student/Student_1/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var m_dataList = data.maldata;
            var f_dataList = data.femdata;
            var categories = data.cate;
            var flst = [], mlst = [];
            var dataResult_1 = [];
            if (m_dataList != null || f_dataList != null) {
                for (var i = 0; i < m_dataList.length; i++) {
                    mlst.push(parseInt(m_dataList[i].col2));
                }
                for (var i = 0; i < f_dataList.length; i++) {
                    flst.push(parseInt(f_dataList[i].col3));
                }
                var mal_data = {
                    name: 'مرد',
                    data: mlst,
                    stack: 'male'
                };
                dataResult_1.push(mal_data);
                var femal_data = {
                    name: 'زن',
                    data: flst,
                    stack: 'female'
                };
                dataResult_1.push(femal_data);
            }
            drawColumnChart_2("", dataResult_1, categories, chartcontainerName, title, "");
        }
    })
}
function Student_2LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "مراسم";
    $.ajax({
        type: "GET",
        url: '/Student/Student_2/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    var newdata = {
                        name: kol_dataList[i].col1,
                        y: parseFloat(kol_dataList[i].col2),
                        count: kol_dataList[i].col2
                    }
                    dataResult_1.push(newdata);
                }
            }
            var fi_data = {
                type: 'pie',
                name: 'Browser share',
                data: dataResult_1
            };
            drawPieChart(fi_data, chartcontainerName, title);
        }
    })
}
function Student_3LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد دانشجویان تحت پوشش برنامه ها و فعالیت های ورزش درون دانشکده";
    $.ajax({
        type: "GET",
        url: '/Student/Student_3/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    var newdata = {
                        name: kol_dataList[i].col1,
                        y: parseFloat(kol_dataList[i].col2),
                        count: kol_dataList[i].col2
                    }
                    dataResult_1.push(newdata);
                }
            }
            var fi_data = {
                type: 'pie',
                name: 'Browser share',
                data: dataResult_1
            };
            drawPieChart(fi_data, chartcontainerName, title);
        }
    })
}
function Student_4LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد دانشجویان تحت پوشش ورزش های قهرمانی";
    $.ajax({
        type: "GET",
        url: '/Student/Student_4/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    var newdata = {
                        name: kol_dataList[i].col1,
                        y: parseFloat(kol_dataList[i].col2),
                        count: kol_dataList[i].col2
                    }
                    dataResult_1.push(newdata);
                }
            }
            var fi_data = {
                type: 'pie',
                name: 'Browser share',
                data: dataResult_1
            };
            drawPieChart(fi_data, chartcontainerName, title);
        }
    })
}
function Student_5LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد انجمن های ورزش دانشجویی";
    $.ajax({
        type: "GET",
        url: '/Student/Student_5/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var dataResult_1 = [];
            if (kol_dataList != null) {
                for (var i = 0; i < kol_dataList.length; i++) {
                    var newdata = {
                        name: kol_dataList[i].col1,
                        y: parseFloat(kol_dataList[i].col2),
                        count: kol_dataList[i].col2
                    }
                    dataResult_1.push(newdata);
                }
            }
            var fi_data = {
                type: 'pie',
                name: 'Browser share',
                data: dataResult_1
            };
            drawPieChart(fi_data, chartcontainerName, title);
        }
    })
}
function Student_6LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد نشریات دانشجویی";
    $.ajax({
        type: "GET",
        url: '/Student/Student_6/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            var ti = $('<h4>' + title + '</h4><br/>');
            if (kol_dataList != null) {
                $("#chrow").empty();
                var baseHtml = "";
                baseHtml += ('<div class="col-md-6 col-xl-3"><div class="card" id="tooltip-container"><div class="card-body">' +
                    '<h4 class="mt-0 font-16">' + 'تعداد نشریات دانشجویی' + '</h4><h2 class="text-primary my-3 text-center">' +
                    '<span data-plugin="counterup">' + kol_dataList + '</span></h2></div></div></div>');
                $("#chrow").append(ti);
                $("#chrow").append(baseHtml);
            }
        }
    })
}
function Student_7LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد تشکلهای اسلامی";
    $.ajax({
        type: "GET",
        url: '/Student/Student_7/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            if (kol_dataList != null) {
                $("#chrow").empty();
                var ti = $('<h4>' + title + '</h4><br/>');
                var baseHtml = "";
                baseHtml += ('<div class="col-md-6 col-xl-3"><div class="card" id="tooltip-container"><div class="card-body">' +
                    '<h4 class="mt-0 font-16">' + 'تعداد تشکلهای اسلامی' + '</h4><h2 class="text-primary my-3 text-center">' +
                    '<span data-plugin="counterup">' + kol_dataList + '</span></h2></div></div></div>');
                $("#chrow").append(ti);
                $("#chrow").append(baseHtml);
            }
        }
    })
}
function Student_8LoadReport(Doctor_id, startDate, endDate, chartcontainerName) {
    var title = "تعداد انجمن های علمی - دانشجویی";
    $.ajax({
        type: "GET",
        url: '/Student/Student_8/Report',
        data: {
            Doctor_id: Doctor_id != "" ? Doctor_id : 0,
            StartDate: startDate != "" ? startDate : null,
            EndDate: endDate != "" ? endDate : null
        },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var kol_dataList = data.koldata;
            if (kol_dataList != null) {
                var ti = $('<h4>' + title + '</h4><br/>');
                $("#chrow").empty();
                var baseHtml = "";
                baseHtml += ('<div class="col-md-6 col-xl-3"><div class="card" id="tooltip-container"><div class="card-body">' +
                    '<h4 class="mt-0 font-16">' + 'تعداد انجمن های علمی - دانشجویی' + '</h4><h2 class="text-primary my-3 text-center">' +
                    '<span data-plugin="counterup">' + kol_dataList + '</span></h2></div></div></div>');
                $("#chrow").append(ti);
                $("#chrow").append(baseHtml);
            }
        }
    })
}

