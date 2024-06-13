$('head').append('<script async src="https://www.googletagmanager.com/gtag/js?id=UA-90716026-1"></script>');

	window.dataLayer = window.dataLayer || [];
	  function gtag(){dataLayer.push(arguments);}
	  gtag('js', new Date());

	  gtag('config', 'UA-90716026-1');
	  gtag('config', 'AW-974298827');
	  
	  // [ buynow button ] start
	  


function gtag_report_conversion() {
	var url = 'https://1.envato.market/c/1289604/275988/4415?subId1=phoenixcoded&u=https%3A%2F%2Fthemeforest.net%2Fitem%2Fable-pro-responsive-bootstrap-4-admin-template%2F19300403';
	var callback = function () { 
		if (typeof(url) != 'undefined') { 
			 //window.location = url; 
		} 
	}; 
	gtag('event', 'conversion', { 'send_to': 'AW-974298827/HOSOCKjvoJMBEMu9ytAD', 'transaction_id': '', 'event_callback': callback }); 
	//return false;
}


$('head').append('' +
    '<style>' +
        '.fixed-button {position: fixed;bottom: -50px;right: 30px;-webkit-box-shadow: 0 13px 21px rgba(0, 0, 0, 0.15);box-shadow: 0 13px 21px rgba(0, 0, 0, 0.15);opacity: 0;z-index: 9;border-radius: 4px;-webkit-transition: all 0.5s ease;transition: all 0.5s ease;}.fixed-button .btn {margin: 0;background: #79b530;text-decoration:none;text-transform: capitalize;font-size: 15px;padding: 10px 19px;color: #fff;border-radius: 4px;border-bottom: 2px solid #5e8d25;}.fixed-button.active {bottom: 50px;opacity: 1;}' +
    '</style>' +
'');