
$(function () {
    "use strict";

    var CalendarEventList = [];

    function CalendarEvent() {
        this.id = null;
        this.title = null;
        this.start = null;
        this.description = null;
        this.status = null;
        this.color = null;
        this.bgColor = null;
        this.borderColor = null;
        this.dragBgColor = null;
        this.allDay = true;
    }

    function addCalendar(cldr) {
        CalendarEventList.push(cldr);
    }



    function findCalendar(id) {
        var found;

        CalendarEventList.forEach(function (calendar) {
            if (calendar.id === id) {
                found = calendar;
            }
        });

        return found || CalendarEventList[0];
    }

    function hexToRGBA(hex) {
        var radix = 16;
        var r = parseInt(hex.slice(1, 3), radix),
            g = parseInt(hex.slice(3, 5), radix),
            b = parseInt(hex.slice(5, 7), radix),
            a = parseInt(hex.slice(7, 9), radix) / 255 || 1;
        var rgba = 'rgba(' + r + ', ' + g + ', ' + b + ', ' + a + ')';

        return rgba;
    }

    function getRandomColor() {
        let n = (Math.random() * 0xfffff * 1000000).toString(16);
        return '#' + n.slice(0, 6);
    }


    var calendarEl = document.getElementById('calendar');
    var todaydatestring = moment().format('YYYY-MM-DD'); 
    var calendar = new FullCalendar.Calendar(calendarEl, {
        headerToolbar: {
            left: 'prev,next today addEventButton',
            center: 'title',
            right: 'dayGridMonth,dayGridWeek,timeGridDay,listWeek'
        },
        initialView: 'dayGridWeek',
        initialDate: todaydatestring,
        navLinks: true, // can click day/week names to navigate views
        selectable: true,
        nowIndicator: true,
        dayMaxEvents: true, // allow "more" link when too many events
        editable: true,
        selectable: true,
        businessHours: true,
        dayMaxEvents: true, // allow "more" link when too many events
        customButtons: {
            addEventButton: {
                text: 'Add Task',
                click: function () {
                    $('#new-task-modal').modal('show');
                }
            }
        },
        eventClick: function (info) {       
            info.jsEvent.preventDefault();
            showeditmodal(info);
        },
        //events: CalendarEventList, //function () {
        //    return CalendarEventList;
        //}
    });


    function renderCalender() {
        generateCalendarTask().then(function (data) {

            if (data != null) {
                CalendarEventList = [];
                var todo;
                var color;
                data.forEach(function (item) {
                    var taskdate = moment(item.dueDate).format('YYYY-MM-DD');
                    todo = new CalendarEvent();
                    color = getRandomColor()
                    todo.id = item.id;
                    todo.title = item.title;
                    todo.start = taskdate; 
                    todo.description = item.description;
                    todo.status = item.status;
                    todo.color = color;
                    todo.bgColor = color;
                    todo.borderColor = color;
                    todo.dragBgColor = color;
                    addCalendar(todo);
                    calendar.addEvent(todo);
                });                
                calendar.render();
            }
        });

    }

    //document.addEventListener('DOMContentLoaded', function () {
    //    renderCalender();
    //});



    function generateCalendarTask() {
        return new Promise(function (resolve, reject) {
            // Make AJAX call
            $.ajax({
                url: '/api/Tasktodo',
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

    renderCalender();


    $('#new-task-modal').on('show.bs.modal', function (event) {
        var todaydate = moment().format('YYYY-MM-DD');
        $('#frmNewTask').removeClass('was-validated');
        $('#frmNewTask').removeClass('invalid');
        $("#tasksToDo_Title").val('');
        $("#tasksToDo_Description").val('');
        $("#tasksToDo_DueDate").val(todaydate);
        $("#tasksToDo_Status").val('');
        $("#tasksToDo_Id").val('');
        //$('#mdl-add-new-company-checkId-available').html('.');
    });

    $('#btn-mdl-save-newtask').on('click', function (e) {

        if (isNewTaskEntryValid()) {
                        
            var title_1 = $('#tasksToDo_Title').val();
            var descrp = $('#tasksToDo_Description').val();
            var StrTm = $('#tasksToDo_DueDate').val();
            
            let tasksToDo = {
                Id: 0,
                Title: title_1,
                Description: descrp,
                DueDate: StrTm + 'T00:00:00',  // ISO 8601 format
                Status: "P",
                CreateDateTime: moment().format('YYYY-MM-DDTHH:mm:ss')  // Correct ISO format
            };                 

            $.ajax({
                url: '/api/Tasktodo',
                type: 'POST',
                contentType: "application/json",
                dataType: "json",                
                data: JSON.stringify(tasksToDo),  
            }).done(function (data) {
                if (data.success) {
                    $('#new-task-modal').modal('hide');                    
                    alert_success_with_redirect(data.message,"/index");
                } else {                    
                    alert_error(data.message);
                }
            }).fail(function () {
                alert_error('Something went wrong');
            });
        }

    });

    function isNewTaskEntryValid() {
        $('#frmNewTask').removeClass('was-validated');
        $('#frmNewTask').removeClass('invalid');
        let valdfail = false;

        var title = $('#tasksToDo_Title').val();
        var descrp = $('#tasksToDo_Description').val();
        var StrTm = $('#tasksToDo_DueDate').val();

        if (title == '' || descrp == '' || StrTm == '') {
            valdfail = true;
        }


        if (valdfail == true) {
            $('#frmNewTask').addClass('was-validated');
            $('#frmNewTask').addClass('invalid');
            return false;
        }
        return true;
    }

    function showeditmodal(calevent) {
        var cl = findCalendar(calevent.event.id);
        var taskdate = moment(calevent.event.start).format('YYYY-MM-DD');
        $('#mdledit_Id').val(calevent.event.id);
        $('#mdledit_Title').val(calevent.event.title);
        $('#mdledit_Description').val(cl.description);
        $('#mdledit_DueDate').val(taskdate);
        $('#mdledit_Status').val(cl.status);
        $('#edit-task-modal').modal('show');
    }

    $('#btn-mdl-save-edittask').on('click', function (e) {

        if (isEditTaskEntryValid()) {                       

            let tasksToDo = {
                Id: $('#mdledit_Id').val(),
                Title: $('#mdledit_Title').val(),
                Description: $('#mdledit_Description').val(),
                DueDate: $('#mdledit_DueDate').val() + 'T00:00:00',  // ISO 8601 format
                Status: $('#mdledit_Status').val(),
                CreateDateTime: moment().format('YYYY-MM-DDTHH:mm:ss')  // Correct ISO format
            };

            $.ajax({
                url: '/api/Tasktodo',
                type: 'PUT',
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(tasksToDo),
            }).done(function (data) {
                if (data.success) {
                    $('#edit-task-modal').modal('hide');
                    alert_success_with_refresh_window(data.message);
                } else {
                    alert_error(data.message);
                }
            }).fail(function () {
                alert_error('Something went wrong');
            });
        }

    });

    function isEditTaskEntryValid() {
        $('#frmEditTask').removeClass('was-validated');
        $('#frmEditTask').removeClass('invalid');
        let valdfail = false;

        var title = $('#mdledit_Title').val();
        var descrp = $('#mdledit_Description').val();
        var StrTm = $('#mdledit_DueDate').val();

        if (title == '' || descrp == '' || StrTm == '') {
            valdfail = true;
        }


        if (valdfail == true) {
            $('#frmEditTask').addClass('was-validated');
            $('#frmEditTask').addClass('invalid');
            return false;
        }
        return true;
    }

    $('#btn-mdl-delete-edittask').on('click', function (e) {
        const taskId = $('#mdledit_Id').val();
        const taskTitle = $('#mdledit_Title').val();
        var qus = 'Are you sure to delete task "' + taskTitle + '" ?';
        confirm_with_callback(qus, taskId, deleteTask);
    });

    function deleteTask(task_Id) {
        $.ajax({
            url: '/api/Tasktodo/' + task_Id,
            type: 'DELETE',
            dataSrc: '',
            headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
        }).done(function (data) {
            if (data.success) {
                $('#edit-task-modal').modal('hide');
                alert_success_with_refresh_window(data.message);
            } else {
                alert_error(data.message);
            }
        }).fail(function () {
            console.log('error')
        });
    }
    

});