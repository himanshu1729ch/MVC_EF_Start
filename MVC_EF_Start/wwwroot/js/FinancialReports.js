window.onload = function () {
    var chart = new Chart(document.getElementById("bar-chart-grouped"), {
        type: 'bar',
        data: {
            labels: ["2014", "2016", "2018", "2020"],
            datasets: [
                {
                    label: "Cash at the Beginning",
                    backgroundColor: "#3636a1",
                    backdropColor: "white",
                    data: [133, 221, 783, 2478]
                }, {
                    label: "Cash at the End",
                    backgroundColor: "#b72d2d",
                    data: [408, 547, 675, 734]
                }
            ]
        },
        options: {
            legend: {
                labels: {
                    fontColor: "white",
                    fontSize: 18
                }
            },
            scales: {
                yAxes: [{
                    ticks: {
                        fontColor: "white",
                        fontSize: 18
                    }
                }],
                xAxes: [{
                    ticks: {
                        fontColor: "white",
                        fontSize: 20
                    }
                }]
            },
            title: {
                fontSize: 24,
                fontColor: "white",
                display: true,
                text: 'Financial Status Graph (amount in millions)'
            },

        }
    });
}

btnonclick = function () {
    alert("this is the stop");
}