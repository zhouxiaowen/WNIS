
(function ($) {
$.dataTablesToAbpInput = function (request, settings) {
    var input = {};
    input.Sequence = request.draw;
    input.SkipCount = (request.start != null) ? request.start : 0;
    input.MaxResultCount = (request.length != null) ? request.length : settings._iDisplayLength;
    return input;
},

$.dataTablesFromAbpOutput = function (input, output, settings) {
    var response = {};
    response.draw = input.Sequence;
    response.recordsTotal = output.TotalCount;
    response.recordsFiltered = output.TotalCount;
    response.data = output.Items;
    return response;
}

})(jQuery);