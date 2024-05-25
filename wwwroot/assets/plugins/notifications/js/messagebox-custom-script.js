
function alert_error(dispmsg) {
	Lobibox.alert('error', {
        msg: dispmsg,
        //buttons: ['ok', 'cancel', 'yes', 'no'],
        //Or more powerfull way
        buttons: {
            ok: {
                'class': 'btn btn-info',
                closeOnClick: true
            },
            cancel: {
                'class': 'btn btn-info',
                closeOnClick: true
            }//,
            //yes: {
            //    'class': 'btn btn-success',
            //    closeOnClick: false
            //},
            //no: {
            //    'class': 'btn btn-warning',
            //    closeOnClick: false
            //},
            //custom: {
            //    'class': 'btn btn-default',
            //    text: 'Custom'
            //}
        } //,
        //callback: function (lobibox, type) {
        //    var btnType;
        //    if (type === 'no') {
        //        btnType = 'warning';
        //    } else if (type === 'yes') {
        //        btnType = 'success';
        //    } else if (type === 'ok') {
        //        btnType = 'info';
        //    } else if (type === 'cancel') {
        //        btnType = 'error';
        //    }
        //    Lobibox.notify(btnType, {
        //        size: 'mini',
        //        msg: 'This is ' + btnType + ' message'
        //    });
        //}
    });
}

function alert_info(dispmsg) {
    Lobibox.alert('info', {
        msg: dispmsg,
        //buttons: ['ok', 'cancel', 'yes', 'no'],
        //Or more powerfull way
        buttons: {
            ok: {
                'class': 'btn btn-info',
                closeOnClick: true
            }
        } 
    });
}

function alert_success(dispmsg) {
    Lobibox.alert('success', {
        msg: dispmsg,
        //buttons: ['ok', 'cancel', 'yes', 'no'],
        //Or more powerfull way
        buttons: {
            ok: {
                'class': 'btn btn-info',
                closeOnClick: true
            }
        }
    });
}

function alert_success_with_refresh_window(dispmsg) {
    Lobibox.alert('success', {
        msg: dispmsg,
        //buttons: ['ok', 'cancel', 'yes', 'no'],
        //Or more powerfull way
        buttons: {
            ok: {
                'class': 'btn btn-info',
                closeOnClick: true
            }
        },
        callback: function (lobibox, type) {
            location.reload();
        }
    });
}

function alert_success_with_redirect(dispmsg,redirect) {
    Lobibox.alert('success', {
        msg: dispmsg,
        //buttons: ['ok', 'cancel', 'yes', 'no'],
        //Or more powerfull way
        buttons: {
            ok: {
                'class': 'btn btn-info',
                closeOnClick: true
            }
        },
        callback: function (lobibox, type) {
            window.location.replace(redirect);
        }
    });
}

function confirm_with_callback(message,Id,funcToExecute) {
    Lobibox.confirm({
        msg: message,
        callback: function ($this, type, ev) {
            if (type === 'yes') {
                funcToExecute(Id);
            }
        }
    });
}