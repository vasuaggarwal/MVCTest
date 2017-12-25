$("#Dated").kendoDatePicker({   
    format: "dd/MM/yyyy"
});
var priority = [
         { Name: "Priority1", Id: 1 },
         { Name: "Priority2", Id: 2 },
         { Name: "Priority3", Id: 3 },       
];
var costViewModel = kendo.observable(
{
    Cost: {},
    close:function(e)
    {
        $('#modal-container').modal('hide');
    },
    save:function(e)
    {
        var data = this.get("Cost");
        data.Priority = $("#Priority").data("kendoDropDownList").value();
        data.Dated =kendo.toString(data.Dated,"MM/dd/yyyy");
        data.Description = $('#Description').val();
        data.Id = costData.Id;
        console.log(data);
      // if (data.Id == 0) {
            var url = "http://localhost:57489/api/Cost/Save";
            $.ajax({
                url: url,
                type: 'POST',
                data: JSON.stringify(data),
                contentType: "application/json;charset=utf-8",
                success: function (html) {
                    if (html) {
                        $('#modal-container').modal('hide');
                        $("#recordsDiv").empty();
                        $("#recordsDiv").append(html);
                        $("#costGrid").data("kendoGrid").dataSource.read();
                    }
                    else {
                        alert("there has some problem in saving data");
                    }
                }
            });
        //}
        //else
        //{
        //    var url = "http://localhost:57489/api/Cost/Update";
        //    $.ajax({
        //        url: url,
        //        type: 'PUT',
        //        data: JSON.stringify(data),
        //        contentType: "application/json;charset=utf-8",
        //        success: function (html) {
        //            if (html) {
        //                $('#modal-container').modal('hide');
        //                $("#recordsDiv").empty();
        //                $("#recordsDiv").append(html);
        //                $("#costGrid").data("kendoGrid").dataSource.read();
        //            }
        //            else {
        //                alert("there has some problem in saving data");
        //            }
        //        }
        //    });
        //}
    }
});
$("#Priority").kendoDropDownList({
    dataTextField: "Name",
    dataValueField: "Id",
    dataSource: priority,
    index: 1
});

kendo.bind("#costForm", costViewModel);
viewModel.set("Cost", costData);
$(document).ready(function () {
    if (costData.Id > 0) {
        var url = "http://localhost:57489/api/Cost/GetCost/" + costData.Id;
        $.ajax({
            url: url,
            type: 'GET',        
            success: function (result) {
                costViewModel.set("Cost.Name", result.Name);
                costViewModel.set("Cost.EstimatedCost", result.EstimatedCost);
                costViewModel.set("Cost.Dated", result.Dated);
                $("#Description").val(result.Description);
                if(result.Priority==1)
                {
                    $("#Priority").data("kendoDropDownList").value("1");
                    $("#Priority").data("kendoDropDownList").text("Priority1");
                }
                else if (result.Priority == 2) {
                    $("#Priority").data("kendoDropDownList").value("2");
                    $("#Priority").data("kendoDropDownList").text("Priority2");
                }
                else if (result.Priority == 3) {
                    $("#Priority").data("kendoDropDownList").value("3");
                    $("#Priority").data("kendoDropDownList").text("Priority3");
                }
            }
        });
    }
});