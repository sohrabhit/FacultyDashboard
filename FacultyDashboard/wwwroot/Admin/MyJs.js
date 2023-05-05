
const FaToEnNum = s => s.replace(/[۰-۹]/g, d => '۰۱۲۳۴۵۶۷۸۹'.indexOf(d));
function toEnglishDigits(str) {

    // convert persian digits [۰۱۲۳۴۵۶۷۸۹]
    var e = '۰'.charCodeAt(0);
    str = str.replace(/[۰-۹]/g, function (t) {
        return t.charCodeAt(0) - e;
    });

    // convert arabic indic digits [٠١٢٣٤٥٦٧٨٩]
    e = '٠'.charCodeAt(0);
    str = str.replace(/[٠-٩]/g, function (t) {
        return t.charCodeAt(0) - e;
    });
    return str;
}
function ToPersianDateNumber(MiladiDate) {
    /*  var d = MiladiDate.split(' ');// 1/20/2022 5:54:49 PM
      var date = d[0].split('/');
      var time = d[1].split(':');
      //var miladi = jalaali.toJalaali(parseInt(toEnglishDigits(date[2])), parseInt(toEnglishDigits(date[0])), parseInt(toEnglishDigits(date[1])));
      var p = toEnglishDigits(date[2]) + "/" + toEnglishDigits(date[1]) + "/" + + toEnglishDigits(date[0]);
      p += " " + toEnglishDigits(time[0]) + ":" + toEnglishDigits(time[1]) + ":" + toEnglishDigits(time[2]);
      */

    const con = s => s.replace(/[۰-۹]/g, d => '۰۱۲۳۴۵۶۷۸۹'.indexOf(d));
    var p = con(MiladiDate);
    var d = p.split(' ');// 1/20/2022 5:54:49 PM
    var date = d[0].split('/');
    var time = d[1].split(':');
    var q = date[2] + "/" + date[0] + "/" + date[1] + " " + time[0] + ":" + time[1] + ":" + time[2];
    //debugger;
    return p;
}
function ToEnglishNumber(MiladiDate) {
    var d = MiladiDate.split(' ');// 1/20/2022 5:54:49 PM
    var date = d[0].split('/');
    var time = d[1].split(':');
    //var miladi = jalaali.toJalaali(parseInt(toEnglishDigits(date[2])), parseInt(toEnglishDigits(date[0])), parseInt(toEnglishDigits(date[1])));
    var p = toEnglishDigits(date[2]) + "/" + n(toEnglishDigits(date[0])) + "/" + n(toEnglishDigits(date[1])) + " " + toEnglishDigits(time[0]) + ":" + toEnglishDigits(time[1]) + ":" + toEnglishDigits(time[2]);
    //debugger;
    return p;
}
function MiladiToPersian(MiladiDate) {
    var d = MiladiDate.split(' ');// 1/20/2022 5:54:49 PM
    var date = d[0].split('/');
    var time = d[1].split(':');
    var miladi = jalaali.toJalaali(parseInt(toEnglishDigits(date[2])), parseInt(toEnglishDigits(date[0])), parseInt(toEnglishDigits(date[1])));
    var p = miladi.jy + "/" + n(miladi.jm) + "/" + + n(miladi.jd) + " " + n(time[0]) + ":" + n(time[1]) + ":" + n(time[2]);
    //debugger;
    return p;
}
function n(n) {
    return (n > 9 /*&& n == 0*/) ? n : "0" + n;
}
function persianToMiladiShow(persianDate1) {
    //var date = persianDate1.split('/');
    //var y = parseInt(toEnglishDigits(date[0]));
    //var m = parseInt(toEnglishDigits(date[1]));
    //var d = parseInt(toEnglishDigits(date[2]));
    //var de = new persianDate([y, m, d, 0, 0, 0, 0]).toLocale('en').toCalendar('gregorian').format('YYYY/MM/DD hh:mm:ss a');
    var d = persianDate1.split(' ');
    var date = d[0].split('/');
    var y = parseInt(date[2]);
    var m = parseInt(date[0]);
    var d = parseInt(date[1]);
    var de = n(y) + "/" + n(m) + "/" + n(d);
    //var de = new persianDate([y, m, d, 0, 0, 0, 0]).toLocale('en').toCalendar('gregorian').format('YYYY/MM/DD hh:mm:ss a');
    return de;
}
function persianToMiladi(persianDate1) {
    var date = persianDate1.split('/');
    var y = parseInt(toEnglishDigits(date[0]));
    var m = parseInt(toEnglishDigits(date[1]));
    var d = parseInt(toEnglishDigits(date[2]));
    var de = new persianDate([y, m, d, 0, 0, 0, 0]).toLocale('en').toCalendar('gregorian').format('YYYY/MM/DD hh:mm:ss a');
    return de;
}
function persianToMiladiWithTime(persianDate1) {
    var d = persianDate1.split(' ');// $('#inputDate1').val().split('  ');
    var date = d[0].split('/');
    var time = d[1].split(':');
    var y = parseInt(toEnglishDigits(date[0]));
    var m = parseInt(toEnglishDigits(date[1]));
    var d = parseInt(toEnglishDigits(date[2]));
    var h = parseInt(toEnglishDigits(time[0]));
    var min = parseInt(toEnglishDigits(time[1]));
    var s = parseInt(toEnglishDigits(time[2]));
    var de = new persianDate([y, m, d, h, min, s, 0]).toLocale('en').toCalendar('gregorian').format('YYYY/MM/DD hh:mm:ss a');
    //var p = new Date(de);
    return de;
}
function isANumber(n) {
    var numStr = /^-?(\d+\.?\d*)$|(\d*\.?\d+)$/;
    return numStr.test(n.toString());
}
// ApexChart
function BarChart_2(title, subtitle, showLables, data, colors, categories) {
    var options = {
        series: [{
            data: data//[400, 430, 448, 470, 540]
        }],
        chart: {
            type: 'bar',
            height: 380,
            toolbar: {
                show: false,
            }
        },
        plotOptions: {
            bar: {
                barHeight: '100%',
                distributed: true,
                horizontal: true,
                dataLabels: {
                    position: 'bottom'
                },
            }
        },
        colors: colors,//['#33b2df', '#546E7A', '#d4526e', '#13d8aa', '#A5978B'],
        dataLabels: {
            enabled: true,
            textAnchor: 'start',
            style: {
                colors: ['#000000']
            },
            formatter: function (val, opt) {
                return val
            },
            offsetX: 0,
            dropShadow: {
                enabled: true
            }
        },
        stroke: {
            width: 1,
            colors: ['#000000']
        },
        xaxis: {
            categories: categories,//['South Korea', 'Canada', 'United Kingdom', 'Netherlands', 'Italy'],,
		    labels: {
                show: false,
                //style: {
                //    color: '#000000',
                //    fontSize: '18px',
                //    fontWeight: 'bold',
                //}
            },
            axisBorder: {
                show: false,
            },
            axisTicks: {
                show: false,
            }
        },
        yaxis: {
            reversed: true,
            labels: {
                show: showLables,//false // نمایش لیبل سمت راست
                style: {
                    color: '#000000',
                    //fontSize: '15px',
                    right: '25px',
                    fontWeight: 'bold',
                },
          //offsetX: 40,
          //      offsetY: 0,
            }
        },
        title: {
            text: title,//'Custom DataLabels',
            align: 'center',
            floating: true,
            style: {
                color: '#000000',
                fontSize: '18px',
                fontWeight: 'bold',
            }
        },
        subtitle: {
            text: subtitle,//'Category Names as DataLabels inside bars',
            align: 'center',
            style: {
                color: '#000000',
                fontSize: '18px',
                fontWeight: 'bold',
            }
        },
        tooltip: {
            theme: 'dark',
            x: {
                show: false
            },
            y: {
                title: {
                    formatter: function () {
                        return ''
                    }
                }
            }
        }
    };
    return options;
}
function drawdateChart(startdate, datetype, data, elementName, title) {
    Highcharts.dateFormats = {
        'a': function (ts) { return new persianDate(ts).format('dddd') },
        'A': function (ts) { return new persianDate(ts).format('dddd') },
        'd': function (ts) { return new persianDate(ts).format('DD') },
        'e': function (ts) { return new persianDate(ts).format('D') },
        'b': function (ts) { return new persianDate(ts).format('MMMM') },
        'B': function (ts) { return new persianDate(ts).format('MMMM') },
        'm': function (ts) { return new persianDate(ts).format('MM') },
        'y': function (ts) { return new persianDate(ts).format('YY') },
        'Y': function (ts) { return new persianDate(ts).format('YYYY') },
        'W': function (ts) { return new persianDate(ts).format('ww') }
    };
    var tick = 0;
    if (datetype == "day")
        tick = 24 * 3600 * 1000;
    else if (datetype == "month")
        tick = 30 * 24 * 3600 * 1000;
    else if (datetype == "year")
        tick = 365 * 24 * 3600 * 1000;
    else if (datetype == "week")
        tick = 7 * 24 * 36e5;
    Highcharts.chart(elementName, {
        chart: {
            renderTo: elementName//''
        },
        title: {
            text: title//''
        },
        xAxis: {
            type: 'datetime',
            tickInterval: tick
        },
        series: [{
            data: data,
            pointInterval: tick,
            pointStart: startdate//Date.UTC(2013, 0, 7)
        }]

    });
}
function GuageChart2(title, data, chartcontainerName, anglePosition, min, max) {
    Highcharts.chart(chartcontainerName, {
        chart: {
            type: 'gauge',
            plotBackgroundColor: null,
            plotBackgroundImage: null,
            plotBorderWidth: 0,
            plotShadow: false
        },
        title: {
            text: title
        },
        pane: {
            // -90 .. 90 => 180 degree , -150 .. 150 380 degree
            startAngle: -150,
            endAngle: 150,
            background: [{
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0, '#FFF'],
                        [1, '#333']
                    ]
                },
                borderWidth: 0,
                outerRadius: '109%'
            }, {
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0, '#333'],
                        [1, '#FFF']
                    ]
                },
                borderWidth: 1,
                outerRadius: '107%'
            }, {
                // default background
            }, {
                backgroundColor: '#DDD',
                borderWidth: 0,
                outerRadius: '105%',
                innerRadius: '103%'
            }]
        },
        // the value axis
        yAxis: {
            min: min,//0,
            max: max,//200,
            minorTickInterval: 'auto',
            minorTickWidth: 1,
            minorTickLength: 10, // ضخامت دندانه هاع ثانیه
            minorTickPosition: 'inside',
            minorTickColor: '#666',

            tickPixelInterval: 10, // ضخامت دندانه های دقیقه
            tickWidth: 1,
            tickLength: 10,
            tickColor: '#666',
            tickPosition: 'inside',
            tickPositioner: function () {
                var plotBands = this.options.plotBands,
                    ticks = [],
                    i = 0;

                for (i; i <= max; i += (max == 10) ? 1 : 10) { // فاصله تیک اعداد
                    ticks.push(i);
                }

                for (i = 0; i < plotBands.length - 1; i++) {
                    ticks.push(plotBands[i].to)
                }
                return ticks;
            },
            labels: {
                step: 1,
                rotation: 'auto',
                style: {
                    fontWeight: 'bold',
                    fontSize: "15px"
                }
            },
            title: {
                text: ''//'km/h'
            },
            plotBands: data
        },
        series: [{
            name: 'Value',
            data: [anglePosition], // موقعیت فلش
            
            style: {
                fontWeight: 'bold',
                fontSize: "35px"
            },
            tooltip: {
                valueSuffix: ''//' km/h'
            }
        }]
    });
}
function GuageChart3(title, data, chartcontainerName, anglePosition, min, max) {
    Highcharts.chart(chartcontainerName, {
        chart: {
            type: 'gauge',
            plotBackgroundColor: null,
            plotBackgroundImage: null,
            plotBorderWidth: 0,
            plotShadow: false
        },
        title: {
            text: title
        },
        pane: {
            // -90 .. 90 => 180 degree , -150 .. 150 380 degree
            //startAngle: -150,
            //endAngle: 150,
            startAngle: -90,
            endAngle: 90,
            background: [{
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0, '#FFF'],
                        [1, '#333']
                    ]
                },
                borderWidth: 0,
                outerRadius: '109%'
            }, {
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0, '#333'],
                        [1, '#FFF']
                    ]
                },
                borderWidth: 1,
                outerRadius: '107%'
            }, {
                // default background
            }, {
                backgroundColor: '#DDD',
                borderWidth: 0,
                outerRadius: '105%',
                innerRadius: '103%'
            }]
        },
        // the value axis
        yAxis: {
            min: min,//0,
            max: max,//200,
            minorTickInterval: 'auto',
            minorTickWidth: 1,
            minorTickLength: 10, // ضخامت دندانه هاع ثانیه
            minorTickPosition: 'inside',
            minorTickColor: '#666',

            tickPixelInterval: 10, // ضخامت دندانه های دقیقه
            tickWidth: 1,
            tickLength: 10,
            tickColor: '#666',
            tickPosition: 'inside',
            tickPositioner: function () {
                var plotBands = this.options.plotBands,
                    ticks = [],
                    i = 0;

                for (i; i <= max; i += (max==10)?1:10) { // فاصله تیک اعداد
                    ticks.push(i);
                }

                for (i = 0; i < plotBands.length - 1; i++) {
                    ticks.push(plotBands[i].to)
                }
                return ticks;
            },
            labels: {
                step: 1,
                rotation: 'auto',
                //x: -30,
                //y: -30,
                style: {
                    fontWeight: 'bold',
                    fontSize: "15px"
                }
            },
            title: {
                text: ''//'km/h'
            },
            plotBands: data
        },
        series: [{
            name: 'Value',
            data: [anglePosition], // موقعیت فلش
            tooltip: {
                valueSuffix: ''//' km/h'
            },
            style: {
                fontWeight: 'bold',
                fontSize: "35px"
            }
        }]
    });
}
function GuageChart(title, data, chartcontainerName, anglePosition, value) {
    var chartSpeed = Highcharts.chart(chartcontainerName, {
        chart: {
            renderTo: chartcontainerName,
            plotBackgroundColor: null,
            plotBackgroundImage: null,
            plotBorderWidth: 0,
            plotShadow: false
        },
        title: {
            text: title,//'Browser<br>shares<br>2015',
            align: 'center',
            verticalAlign: 'top',
            y: 10
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        pane: {
            center: ['50%', '50%'],
            size: '100%',
            startAngle: -90,
            endAngle: 90,
            background: {
                borderWidth: 0,
                backgroundColor: 'none',
                innerRadius: '60%',
                outerRadius: '100%',
                shape: 'arc'
            }
        },
        yAxis: [{
            lineWidth: 0,
            min: 0,
            max: 90,
            minorTickLength: 0,
            tickLength: 0,
            tickWidth: 0,
            labels: {
                enabled: false
            },
            title: {
                text: '<div class="gaugeFooter"><h3>' + value + '</h3></div>',
                useHTML: true,
                y: 50
            },
            pane: 0,
        }],
        plotOptions: {
            pie: {
                dataLabels: {
                    enabled: true,
                    distance: -50,
                    style: {
                        fontWeight: 'bold',
                        color: 'white',
                        textShadow: '0px 1px 2px black'
                    }
                },
                startAngle: -90,
                endAngle: 90,
                center: ['50%', '50%']
            },
            gauge: {
                dataLabels: {
                    enabled: false
                },
                dial: {
                    radius: '100%'
                }
            }
        },
        series: [{
            type: 'pie',
            name: 'Browser share',
            innerSize: '50%',
            data: data
        }, {
            type: 'gauge',
            data: [anglePosition], // موقعیت فلش
            dial: {
                rearLength: 0
            }
        }],
    });
}
function drawColumnDateChart(startdate, datetype, data, elementName, y_title, pointer_title, title, subject) {
    Highcharts.dateFormats = {
        'a': function (ts) { return new persianDate(ts).format('dddd') },
        'A': function (ts) { return new persianDate(ts).format('dddd') },
        'd': function (ts) { return new persianDate(ts).format('DD') },
        'e': function (ts) { return new persianDate(ts).format('D') },
        'b': function (ts) { return new persianDate(ts).format('MMMM') },
        'B': function (ts) { return new persianDate(ts).format('MMMM') },
        'm': function (ts) { return new persianDate(ts).format('MM') },
        'y': function (ts) { return new persianDate(ts).format('YY') },
        'Y': function (ts) { return new persianDate(ts).format('YYYY') },
        'W': function (ts) { return new persianDate(ts).format('ww') }
    };
    var tick = 0;
    if (datetype == "day")
        tick = 24 * 3600 * 1000;
    else if (datetype == "mon")
        tick = 30 * 24 * 3600 * 1000;
    else if (datetype == "year")
        tick = 365 * 24 * 3600 * 1000;
    else if (datetype == "week")
        tick = 7 * 24 * 36e5;
    Highcharts.chart(elementName, {
        chart: {
            type: 'column',
            options3d: {
                enabled: true,
                alpha: 15,
                beta: 15,
                viewDistance: 25,
                depth: 40
            }
        },
        title: {
            text: title//'Total fruit consumption, grouped by gender'
        },
        xAxis: {
            type: 'datetime',
            tickInterval: tick
        },
        yAxis: {
            allowDecimals: false,
            min: 0,
            title: {
                text: y_title/*'Average'*/,
                skew3d: true
            }
        },
        tooltip: {
            headerFormat: '<b>' + subject + '</b><br>',//'<b>{point.key}</b><br>',
            pointFormat: '<span style="color:{series.color}">\u25CF</span> '/*{series.name}*/ + pointer_title +': {point.y}'// / {point.stackTotal}'
        },
        plotOptions: {
            column: {
                //stacking: 'normal',
                depth: 40
            }
        },

        //series: data
        series: [{
            name: 'Percentage',
            data: data,//[2, 3, null, 4, 0, 5, 1, 4, 6, 3],
            pointInterval: tick,
            pointStart: startdate//Date.UTC(2013, 0, 7)
        }]
    });

}

function convertToMonth(monNomber) {
    let months = new Array("فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دي", "بهمن", "اسفند");
    return months[monNomber - 1];
}
function draw_Custom_ColumnChart(data, elementName, title, subject) {
    Highcharts.chart(elementName, {
        chart: {
            type: 'column',
            options3d: {
                enabled: true,
                alpha: 15,
                beta: 15,
                viewDistance: 25,
                depth: 40
            }
        },
        title: {
            text: title
        },
        subtitle: {
            text: subject
        },
        accessibility: {
            announceNewData: {
                enabled: true
            }
        },
        xAxis: {
            type: 'category'
        },
        yAxis: {
            title: {
                text: 'Average'
            }

        },
        legend: {
            enabled: false
        },
        plotOptions: {
            series: {
                borderWidth: 0,
                dataLabels: {
                    enabled: true,
                    format: '{point.y:.1f}' // '{point.y:.1f}%'
                }
            }
        },

        tooltip: {
            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> percent<br/>'
        },

        series: [
            {
                name: "Browsers",
                colorByPoint: true,
                data: data
            }
        ],
    });
}

function ChartColumnWithLine(chartcontainerName, title, category, columndata, linedata) {
   /* var chartSpeed =*/ Highcharts.chart(chartcontainerName, {
        chart: {
            zoomType: 'xy'
        },
        title: {
            text: title//'Average Monthly Temperature and Rainfall in Tokyo'
        },
        //subtitle: {
        //    text: 'Source: WorldClimate.com'
        //},

        xAxis: [{
            categories: category,//['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            crosshair: true
        }],
        yAxis: [{ // Primary yAxis
            labels: {
                format: '{value}',// '{value}°C',
                style: {
                    color: Highcharts.getOptions().colors[1]
                }
            },
            title: {
                text: 'مقدار',
                style: {
                    color: Highcharts.getOptions().colors[1]
                }
            }
        }, { // Secondary yAxis
            title: {
                text: 'وضعیت مطلوب',
                style: {
                    color: Highcharts.getOptions().colors[0]
                }
            },
            labels: {
                format: '{value}',// '{value} mm',
                style: {
                    color: Highcharts.getOptions().colors[0]
                }
            },
            opposite: true
        }],
        tooltip: {
            shared: true
        },
        legend: {
            layout: 'vertical',
            align: 'left',
            x: 120,
            verticalAlign: 'top',
            y: 100,
            floating: true,
            backgroundColor:
                Highcharts.defaultOptions.legend.backgroundColor || // theme
                'rgba(255,255,255,0.25)'
        },
        series: [{
            name: 'مقدار',
            type: 'column',
            yAxis: 1,
            data: columndata,//[49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4],
            //tooltip: {
            //    valueSuffix: ' mm'
            //}

        }, {
            name: 'وضعیت مطلوب',
            type: 'spline',
            data: linedata,//[7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6],
            //tooltip: {
            //    valueSuffix: '°C'
            //}
        }]
    });
}
function drawColumnChart(doctorNames, data, category, elementName, title, subject) {
    Highcharts.chart(elementName, {
        chart: {
            type: 'column',
            options3d: {
                enabled: true,
                alpha: 15,
                beta: 15,
                viewDistance: 25,
                depth: 40
            }
        },

        title: {
            text: title//'Total fruit consumption, grouped by gender'
        },

        xAxis: {
            categories: category,//['Value 1', 'Value 2']/*doctorNames*/,//['Apples', 'Oranges', 'Pears', 'Grapes', 'Bananas'],
            labels: {
                skew3d: true,
                style: {
                    fontSize: '16px'
                }
            }
        },

        yAxis: {
            allowDecimals: false,
            min: 0,
            title: {
                text: 'Average',
                skew3d: true
            }
        },

        tooltip: {
            headerFormat: '<b>' + subject + '</b><br>',//'<b>{point.key}</b><br>',
            pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y}'// / {point.stackTotal}'
        },

        plotOptions: {
            column: {
                stacking: 'normal',
                depth: 40
            }
        },

        series: data/*[data{
                                            name: 'First Episode',
                                            data: [3, 4, 4, 2, 5],
                                            stack: 'male'
                                        }, {
                                            name: 'Followup',
                                            data: [3, 0, 4, 4, 3],
                                            stack: 'female'
                                        }]*/
    });
}
function drawPieChart(data, elementName, title) {
    Highcharts.chart(elementName, {
        chart: {
            type: 'pie',
            options3d: {
                enabled: true,
                alpha: 45,
                beta: 0
            }
        },
        title: {
            text: title//'Pecentage of all patients'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        tooltip: {
            //pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            pointFormat: '{series.name}: <b>percentage :{point.percentage:.1f}%</b><b> count :{point.count:.1f}</b>'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                depth: 35,
                dataLabels: {
                    enabled: true,
                    format: '{point.name}'
                }
            }
        },
        series: [data/*{
                                 type: 'pie',
                                 name: 'Browser share',
                                 data: [
                                     ['Firefox', 45.0],
                                     ['IE', 26.8],
                                     {
                                         name: 'Chrome',
                                         y: 12.8,
                                         sliced: true,
                                         selected: true
                                     },
                                     ['Safari', 8.5],
                                     ['Opera', 6.2],
                                     ['Others', 0.7]
                                 ]
                             }*/]
    });
}
function gregorian_to_jalali(gy, gm, gd) {
    var g_d_m = [0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334];
    var jy = (gy <= 1600) ? 0 : 979;
    gy -= (gy <= 1600) ? 621 : 1600;
    var gy2 = (gm > 2) ? (gy + 1) : gy;
    var days = (365 * gy) + (parseInt((gy2 + 3) / 4)) - (parseInt((gy2 + 99) / 100))
        + (parseInt((gy2 + 399) / 400)) - 80 + gd + g_d_m[gm - 1];
    jy += 33 * (parseInt(days / 12053));
    days %= 12053;
    jy += 4 * (parseInt(days / 1461));
    days %= 1461;
    jy += parseInt((days - 1) / 365);
    if (days > 365) days = (days - 1) % 365;
    var jm = (days < 186) ? 1 + parseInt(days / 31) : 7 + parseInt((days - 186) / 30);
    var jd = 1 + ((days < 186) ? (days % 31) : ((days - 186) % 30));
    return [jy, jm, jd];
}
function jalali_to_gregorian(jy, jm, jd) {
    var gy = (jy <= 979) ? 621 : 1600;
    jy -= (jy <= 979) ? 0 : 979;
    var days = (365 * jy) + ((parseInt(jy / 33)) * 8) + (parseInt(((jy % 33) + 3) / 4))
        + 78 + jd + ((jm < 7) ? (jm - 1) * 31 : ((jm - 7) * 30) + 186);
    gy += 400 * (parseInt(days / 146097));
    days %= 146097;
    if (days > 36524) {
        gy += 100 * (parseInt(--days / 36524));
        days %= 36524;
        if (days >= 365) days++;
    }
    gy += 4 * (parseInt((days) / 1461));
    days %= 1461;
    gy += parseInt((days - 1) / 365);
    if (days > 365) days = (days - 1) % 365;
    var gd = days + 1;
    var sal_a = [0, 31, ((gy % 4 == 0 && gy % 100 != 0) || (gy % 400 == 0)) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    var gm
    for (gm = 0; gm < 13; gm++) {
        var v = sal_a[gm];
        if (gd <= v) break;
        gd -= v;
    }
    return [gy, gm, gd];
}

function drawColumnChart_2(doctorNames, data, category, elementName, title, subject) {
    Highcharts.chart(elementName, {
        chart: {
            type: 'column'
        },
        title: {
            text: title//'Total fruit consumption, grouped by gender'
        },
        //subtitle: {
        //    text: 'Source: WorldClimate.com'
        //},
        xAxis: {
            categories: category//['Apples', 'Oranges', 'Pears', 'Grapes', 'Bananas']
        },
        yAxis: {
            allowDecimals: false,
            min: 0,
            title: {
                text: 'تعداد'
            }
        },
        plotOptions: {
            column: {
                stacking: 'normal'
            }
        },
        credits: {
            enabled: false
        },
        series: data/*[
            {
                name: 'مرد',
                data: [5, 3, 4, 7, 2],
                stack: 'male'
            },
            //{
            //    name: 'Joe',
            //    data: [3, 4, 4, 2, 5],
            //    stack: 'male'
            //},
            {
                name: 'زن',
                data: [2, 5, 6, 2, 1],
                stack: 'female'
            }//,
            //{
            //    name: 'Janet',
            //    data: [3, 0, 4, 4, 3],
            //    stack: 'female'
            //}
        ]*/
    });
}
function drawColumnChart_3(doctorNames, data, category, elementName, title, subtitle) {
    Highcharts.chart(elementName, {
        chart: {
            type: 'column',
            options3d: {
                enabled: true,
                alpha: 15,
                beta: 15,
                viewDistance: 25,
                depth: 40
            }
        },

        title: {
            text: title,//' Electricity production in countries, grouped by continent',
            align: 'center'
        },
        subtitle: {
            text: subtitle//'Source: WorldClimate.com'
        },

        xAxis: {
            //labels: {
            //    skew3d: true,
            //    style: {
            //        fontSize: '16px'
            //    }
            //},
            categories: //category
                    ['گروه علوم آزمایشگاهی', 'گروه رادیولوژی', 'گروه هوشبری', 'گروه فناوری اطلاعات سلامت', 'گروه اتاق عمل', 'گروه فوریتهای پزشکی', 'گروه زبان']
        },

        yAxis: {
            allowDecimals: false,
            min: 0,
            title: {
                text: 'تعداد مقالات',
                skew3d: true,
                style: {
                    fontSize: '16px'
                }
            }
        },

        tooltip: {
            headerFormat: '<b>{point.key}</b><br>',
            pointFormat: '<span style="color:{series.color}">\u25CF</span> {series.name}: {point.y} / {point.stackTotal}'
        },

        plotOptions: {
            //series: {
            //    pointStart: 2016
            //},
            column: {
                stacking: 'normal',
                depth: 40
            }
        },

        series: data
        /*[{
            name: 'pubmed',
            data: [12, 0, 0, 0, 0, 0, 0],
            stack: 'Asia'
        }, {
            name: 'scopus',
            data: [0, 50, 0, 100, 0, 0, 0],
            stack: 'Asia'
        }, {
            name: 'ISI',
            data: [0, 0, 40, 0, 70, 0, 0],
            stack: 'Asia'
        }, {
            name: 'علمی - پژوهشی',
            data: [20, 30, 0, 0, 0, 80, 0],
            stack: 'Asia'
        }, {
            name: 'علمی - ترویجی',
            data: [0, 15, 0, 40, 0, 50, 0],
            stack: 'Asia'
        }, {
            name: 'کنفرانس های داخلی',
            data: [10, 30, 20, 0, 40, 0, 100],
            stack: 'Asia'
        }, {
            name: 'کنفرانس های بین المللی',
            data: [40, 30, 0, 10, 0, 50, 0],
            stack: 'Asia'
        }]*/
    //});









    //Highcharts.chart(elementName, {
    //    chart: {
    //        type: 'column'
    //    },

    //    title: {
    //        text: 'Total fruit consumtion, grouped by gender'
    //    },

    //    xAxis: {
    //        offset: -43,
    //        labels: {
    //            y: 40
    //        },
    //        categories: ['گروه علوم آزمایشگاهی', 'گروه رادیولوژی', 'گروه هوشبری', 'گروه فناوری اطلاعات سلامت', 'گروه اتاق عمل', 'گروه فوریتهای پزشکی', 'گروه زبان']
    //    },

    //    yAxis: {
    //        allowDecimals: false,
    //        min: -2,
    //        title: {
    //            text: 'Number of fruits'
    //        },
    //        stackLabels: {
    //            verticalAlign: "bottom",
    //            y: 20,
    //            enabled: true,
    //            formatter: function () {
    //                return this.stack;
    //            }
    //        }
    //    },

    //    tooltip: {
    //        formatter: function () {
    //            return '<b>' + this.x + '</b><br/>' +
    //                this.series.name + ': ' + this.y + '<br/>' +
    //                'Total: ' + this.point.stackTotal;
    //        }
    //    },

    //    plotOptions: {
    //        column: {
    //            stacking: 'normal'
    //        }
    //    },

    //    series: data
    //        //[{
    //        //    name: 'John',
    //        //    data: [5, 3, 4, 7, 2],
    //        //    stack: 'male'
    //        //}, {
    //        //        name: 'Joe',
    //        //        data: [3, 4, 4, 2, 5],
    //        //        stack: 'male'
    //        //    }, {
    //        //        name: 'Jane',
    //        //        data: [2, 5, 6, 2, 1],
    //        //    stack: 'male'
    //        //    }, {
    //        //        name: 'Janet',
    //        //        data: [3, 0, 4, 4, 3],
    //        //    stack: 'male'
    //        //    }]
        

    //Highcharts.chart(elementName, {
    //    chart: {
    //        type: 'column'
    //    },
    //    title: {
    //        text: 'Monthly Average Rainfall'
    //    },
    //    subtitle: {
    //        text: 'Source: WorldClimate.com'
    //    },
    //    xAxis: {
    //        categories: ['گروه علوم آزمایشگاهی', 'گروه رادیولوژی', 'گروه هوشبری', 'گروه فناوری اطلاعات سلامت', 'گروه اتاق عمل', 'گروه فوریتهای پزشکی', 'گروه زبان'],
    //        crosshair: true
    //    },
    //    yAxis: {
    //        min: 0,
    //        title: {
    //            text: 'Rainfall (mm)'
    //        }
    //    },
    //    tooltip: {
    //        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
    //        pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
    //            '<td style="padding:0"><b>{point.y:.1f} mm</b></td></tr>',
    //        footerFormat: '</table>',
    //        shared: true,
    //        useHTML: true
    //    },
    //    plotOptions: {
    //        column: {
    //            pointPadding: 0.2,
    //            borderWidth: 0
    //        }
    //    },
    //    series:data
    //        /*[{
    //        name: 'Tokyo',
    //        data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4,
    //            194.1, 95.6, 54.4]

    //    }, {
    //        name: 'New York',
    //        data: [83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5,
    //            106.6, 92.3]

    //    }, {
    //        name: 'London',
    //        data: [48.9, 38.8, 39.3, 41.4, 47.0, 48.3, 59.0, 59.6, 52.4, 65.2, 59.3,
    //            51.2]

    //    }, {
    //        name: 'Berlin',
    //        data: [42.4, 33.2, 34.5, 39.7, 52.6, 75.5, 57.4, 60.4, 47.6, 39.1, 46.8,
    //            51.1]

    //    }]*/
    });
}
