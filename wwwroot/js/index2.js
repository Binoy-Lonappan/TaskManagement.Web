$(function () {
    "use strict";




    function renderChart() {
        generateCalendarChart().then(function (data) {

            if (data != null) {
                if (data.cydata != null)
                    generateCYChart(data.cydata);

                if (data.tdata != null)
                    generateTChart(data.tdata);

            }
        });

    }


    function generateCYChart(d) {

        var s = [];
        var l = [];

        d.forEach(function (item) {
            s.push(item.c_ount);
            l.push(item.status);
        });


        var c = {
            series: s,
            chart: {
                height: 340,
                type: "donut"
            },
            labels: l,
            fill: {
                opacity: 1
            },
            stroke: {
                width: 1,
                colors: void 0
            },
            colors: ["#17a00e", "#f41127", "#0dcaf0", "#ffc107", "#0d6efd"],
            yaxis: {
                show: !1
            },
            dataLabels: {
                enabled: !1
            },
            legend: {
                show: !1,
                position: "bottom"
            },
            plotOptions: {
                polarArea: {
                    rings: {
                        strokeWidth: 0
                    }
                }
            }
        };
        new ApexCharts(document.querySelector("#chartCY"), c).render();

    }

    function generateTChart(d) {

        var s = [];
        var l = [];

        d.forEach(function (item) {
            s.push(item.c_ount);
            l.push(item.status);
        });

        var t = {
            series: s,
            chart: {
                height: 340,
                type: "polarArea"
            },
            labels: l,
            fill: {
                opacity: 1
            },
            stroke: {
                width: 1,
                colors: void 0
            },
            colors: ["#17a00e", "#f41127", "#0dcaf0", "#ffc107", "#0d6efd"],
            yaxis: {
                show: !1
            },
            dataLabels: {
                enabled: !1
            },
            legend: {
                show: !1,
                position: "bottom"
            },
            plotOptions: {
                polarArea: {
                    rings: {
                        strokeWidth: 0
                    }
                }
            }
        };
        new ApexCharts(document.querySelector("#chartT"), t).render();


    }

    function generateCalendarChart() {
        return new Promise(function (resolve, reject) {
            // Make AJAX call
            $.ajax({
                url: '/api/Tasktodo/0',
                type: 'GET',
                dataSrc: '',
                success: function (data) {
                    // Resolve the promise when AJAX call succeeds
                    resolve(data);
                },
                error: function (xhr, status, error) {
                    // Reject the promise if AJAX call fails
                    reject(error);
                }
            });
        });
    }

    renderChart();

});