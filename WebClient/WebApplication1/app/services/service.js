app.factory('stopwatchService', function ($timeout) {
    var SW_DELAI = 100;

    var data = {
        value: 0
    },
        stopwatch = null;

    var start = function () {;
        stopwatch = $timeout(function () {
            data.value++;
            start();
        }, SW_DELAI);
    };

    var stop = function () {
        $timeout.cancel(stopwatch);
        stopwatch = null;
    };

    var reset = function () {
        stop();
        data.value = 0;
    };
    
    return {
        data: data,
        start: start,
        stop: stop,
        reset: reset
    };

});

app.factory('convertExcelToGridService', function() {

    var convert2Grid = function(excelText) {

        CSVParser.resetLog();
        var parseOutput = CSVParser.parse(excelText, true, 'auto', false, false);
        return parseOutput;
    };

    return { convert2Grid: convert2Grid };
});