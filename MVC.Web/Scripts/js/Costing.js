var costDatasource = new kendo.data.DataSource({
    transport: {
        read: {
            url: "http://localhost:57489/api/Cost/GetGridData",
            type: "GET",
     //       crossDomain: true,
           // contentType: 'application/json'
       //      dataType: 'jsonp',
            //beforeSend: function (xhr) {
            //    xhr.withCredentials = true;
            //    xhr.setRequestHeader('Authorization');
            //}
        }
    },
    schema: {
      //  data: "Data",
        model: {
            id: "Id",
            fields: {
            Id: { type: "number" },
            Name: { type: "string" },
            Description: { type: "string" },
            Priority: { type: "string" },
            Dated: { type: "date" },
    }
        }
    },
    type: "aspnetmvc-ajax",
});

var viewModel = kendo.observable(
{
    costDs:costDatasource,
    selectedRow: null,
    change: function (eventArgs) {
        this.set("selectedRow", eventArgs.sender.dataItem(eventArgs.sender.select()));
    },
    addCost:function(e)
    {
        var model = this.get("selectedRow");
        var url="";
        var _data = null;
        if (model != null)
        {
            url = _rootUrl + "Cost/Edit/"+ model.Id;
           // _data = {Id:model.Id};
        }
        else            
        url = _rootUrl + "Cost/Create";
       
        $.ajax({
            url: url,
            type: 'GET',
            data: _data,
            success: function (html) {
                var dd = $("#modal-container").find(".modal-content");
                dd.empty();
                dd.append(html);
                $("#modal-container").modal('show');
            }
        });
    },
    delCost:function(e)
    {
        var model = this.get("selectedRow");
        if(model!=null)
        {
            _data = { id: model.Id };
            var url = "http://localhost:57489/api/Cost/Delete/"+model.Id;
            $.ajax({
                url: url,
                type: 'DELETE', 
                contentType: "application/json;charset=utf-8",
                success: function (html) {
                    $("#costGrid").data("kendoGrid").dataSource.read();
                    alert(html);
                }
            });
        }
        else
        {
            alert("Please select data before delete");
        }
    }
});
$("#costGrid").kendoGrid({
    selectable: true,
    scrollable: true,
    pageable: { refresh: true },
    filterable: true,
    columns: [
          { field: "Name", title: "Name", filterable: { extra: false }, width: 190 },
          { field: "Dated", title: "Date", format: "{0:dd-MMM-yyyy}", filterable: { extra: false }, width: 100 },
          { field: "EstimatedCost", title: "Estimated Cost", filterable: { extra: false }, width: 100, format: "{0:n2}" },
          {
              field: "Priority", title: "Priority", filterable: { extra: false }, width: 100,
              template:function(e)
              {
                  if (e.Priority == 1)
                      return "<div>Priority1</div>";
                  else if (e.Priority == 2)
                      return "<div>Priority2</div>";
                  else
                      return "<div>Priority3</div>";
              }
          },
          { field: "Description", title: "Description", filterable: { extra: false }, width: 190 },
        ],
});
kendo.bind("#costDetail", viewModel);
