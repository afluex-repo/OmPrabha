jQuery(function ($) {
    'use strict';
	
	// Header Sticky
	$(window).on('scroll',function() {
		if ($(this).scrollTop() > 350){  
			$('.navbar-area').addClass("is-sticky");
		}
		else{
			$('.navbar-area').removeClass("is-sticky");
		}
	});

	// Mean Menu
	jQuery('.mean-menu').meanmenu({
		meanScreenWidth: "1199"
	});
	
	// Others Option For Responsive JS
	$(".others-option-for-responsive .dot-menu").on("click", function(){
		$(".others-option-for-responsive .container .container").toggleClass("active");
	});

	// Button Hover JS
	$('.default-btn')
	.on('mouseenter', function(e) {
		var parentOffset = $(this).offset(),
		relX = e.pageX - parentOffset.left,
		relY = e.pageY - parentOffset.top;
		$(this).find('span').css({top:relY, left:relX})
	})
	.on('mouseout', function(e) {
		var parentOffset = $(this).offset(),
		relX = e.pageX - parentOffset.left,
		relY = e.pageY - parentOffset.top;
		$(this).find('span').css({top:relY, left:relX})
	});

	// Home Slides
	$('.home-slides').owlCarousel({
		items: 1,
		loop: true,
		nav: true,
		dots: false,
		smartSpeed: 500,
		margin: 30,
		autoHeight: true,
		animateOut: 'fadeOut',
		animateIn: 'fadeIn',
		autoplayHoverPause: true,
		autoplay: true,
		navText: [
			"<i class='bx bx-left-arrow-alt'></i>",
			"<i class='bx bx-right-arrow-alt'></i>"
		],
	});

	// Home Slides Two
	$('.home-slides-two').owlCarousel({
		items: 1,
		loop: true,
		nav: true,
		dots: true,
		smartSpeed: 500,
		margin: 30,
		autoHeight: true,
		animateOut: 'fadeOut',
		animateIn: 'fadeIn',
		autoplayHoverPause: true,
		autoplay: true,
		navText: [
			"<i class='bx bx-left-arrow-alt'></i>",
			"<i class='bx bx-right-arrow-alt'></i>"
		],
	});

	// Featured Slides
	$('.featured-slides').owlCarousel({
		loop: true,
		nav: true,
		dots: false,
		smartSpeed: 500,
		margin: 30,
		autoHeight: true,
		autoplayHoverPause: true,
		autoplay: true,
		navText: [
			"<i class='bx bx-left-arrow-alt'></i>",
			"<i class='bx bx-right-arrow-alt'></i>"
		],
		responsive: {
			0: {
				items: 1
			},
			768: {
				items: 2
			},
			1200: {
				items: 3
			}
		}
	});
	
	// Case Study Slides
	$('.case-study-slides').owlCarousel({
		loop: true,
		nav: true,
		dots: false,
		smartSpeed: 500,
		margin: 30,
		autoHeight: true,
		autoplayHoverPause: true,
		autoplay: true,
		navText: [
			"<i class='bx bx-left-arrow-alt'></i>",
			"<i class='bx bx-right-arrow-alt'></i>"
		],
		responsive: {
			0: {
				items: 1
			},
			768: {
				items: 2
			},
			1200: {
				items: 3
			}
		}
	});

	// Testimonial Slides
	$('.testimonial-slides').owlCarousel({
		loop: true,
		nav: true,
		dots: false,
		smartSpeed: 500,
		margin: 30,
		autoHeight: true,
		items: 1,
		autoplayHoverPause: true,
		autoplay: true,
		navText: [
			"<i class='bx bx-left-arrow-alt'></i>",
			"<i class='bx bx-right-arrow-alt'></i>"
		],
	});
	$(".testimonial-slides").on("translate.owl.carousel", function(){
		$(".testimonial-item i").removeClass("animated fadeInUp").css("opacity", "0");
		$(".testimonial-item p").removeClass("animated fadeInUp").css("opacity", "0");
		$(".testimonial-item .info").removeClass("animated fadeInUp").css("opacity", "0");
		$(".testimonial-image").removeClass("animated fadeInRight").css("opacity", "0");
	});
	$(".testimonial-slides").on("translated.owl.carousel", function(){
		$(".testimonial-item i").addClass("animated fadeInUp").css("opacity", "1");
		$(".testimonial-item p").addClass("animated fadeInUp").css("opacity", "1");
		$(".testimonial-item .info").addClass("animated fadeInUp").css("opacity", "1");
		$(".testimonial-image").addClass("animated fadeInRight").css("opacity", "1");
	});

	// Blog Slides
	$('.blog-slides').owlCarousel({
		loop: true,
		nav: false,
		autoHeight: true,
		dots: true,
		smartSpeed: 500,
		margin: 30,
		autoplayHoverPause: true,
		autoplay: true,
		navText: [
			"<i class='bx bx-left-arrow-alt'></i>",
			"<i class='bx bx-right-arrow-alt'></i>"
		],
		responsive: {
			0: {
				items: 1
			},
			768: {
				items: 2
			},
			1200: {
				items: 3
			}
		}
	});

	// Blog Slides Two
	$('.blog-slides-two').owlCarousel({
		loop: true,
		items: 1,
		nav: false,
		autoHeight: true,
		dots: true,
		smartSpeed: 500,
		margin: 30,
		autoplayHoverPause: true,
		autoplay: true,
		navText: [
			"<i class='bx bx-left-arrow-alt'></i>",
			"<i class='bx bx-right-arrow-alt'></i>"
		],
	});

	// Agents Slides
	$('.agents-slides').owlCarousel({
		loop: true,
		nav: true,
		dots: false,
		smartSpeed: 500,
		margin: 30,
		autoHeight: true,
		autoplayHoverPause: true,
		autoplay: true,
		navText: [
			"<i class='bx bx-left-arrow-alt'></i>",
			"<i class='bx bx-right-arrow-alt'></i>"
		],
		responsive: {
			0: {
				items: 1
			},
			768: {
				items: 2
			},
			1200: {
				items: 3
			}
		}
	});

	// Customers Slides
	$('.customers-slides').owlCarousel({
		loop: true,
		nav: true,
		dots: false,
		autoHeight: true,
		smartSpeed: 500,
		margin: 20,
		autoplayHoverPause: true,
		autoplay: true,
		navText: [
			"<i class='bx bx-left-arrow-alt'></i>",
			"<i class='bx bx-right-arrow-alt'></i>"
		],
		responsive: {
			0: {
				items: 1
			},
			768: {
				items: 2
			},
			1200: {
				items: 4
			}
		}
	});

	// Property Details Slides
	$('.property-details-slides').owlCarousel({
		loop: true,
		nav: true,
		dots: false,
		smartSpeed: 500,
		margin: 20,
		autoHeight: true,
		autoplayHoverPause: true,
		autoplay: true,
		navText: [
			"<i class='bx bx-left-arrow-alt'></i>",
			"<i class='bx bx-right-arrow-alt'></i>"
		],
		responsive: {
			0: {
				items: 1
			},
			768: {
				items: 2
			},
			1200: {
				items: 2
			}
		}
	});

	// Top Property Slides
	$('.top-properties-slides').owlCarousel({
		loop: true,
		nav: false,
		dots: true,
		items: 1,
		autoHeight: true,
		smartSpeed: 500,
		margin: 20,
		autoplayHoverPause: true,
		autoplay: true,
	});
	
	// Range Slider
	$( "#range-slider" ).slider({
		range: true,
		min: 100,
		max: 20000,
		values: [100, 20000],
		slide: function( event, ui ) {
			$( "#price-amount" ).val( "$" + ui.values[ 0 ] + "-$" + ui.values[ 1 ] );
		}
	});
	$( "#price-amount" ).val( "$" + $( "#range-slider" ).slider( "values", 0 ) +
	" - $" + $( "#range-slider" ).slider( "values", 1 ) );
	
	// Range Slider 2
	$( "#range-slider-2" ).slider({
		range: true,
		min: 100,
		max: 20000,
		values: [100, 20000],
		slide: function( event, ui ) {
			$( "#price-amount-2" ).val( "$" + ui.values[ 0 ] + "-$" + ui.values[ 1 ] );
		}
	});
	$( "#price-amount-2" ).val( "$" + $( "#range-slider-2" ).slider( "values", 0 ) +
	" - $" + $( "#range-slider-2" ).slider( "values", 1 ) );

	// Tabs JS
	$('.tab ul.tabs').addClass('active').find('> li:eq(0)').addClass('current');
	$('.tab ul.tabs li').on('click', function (g) {
		var tab = $(this).closest('.tab'), 
		index = $(this).closest('li').index();
		tab.find('ul.tabs > li').removeClass('current');
		$(this).closest('li').addClass('current');
		tab.find('.tab_content').find('div.tabs_item').not('div.tabs_item:eq(' + index + ')').slideUp();
		tab.find('.tab_content').find('div.tabs_item:eq(' + index + ')').slideDown();
		g.preventDefault();
	});

	// Subscribe form
	$(".newsletter-form").validator().on("submit", function (event) {
		if (event.isDefaultPrevented()) {
			formErrorSub();
			submitMSGSub(false, "Please enter your email correctly.");
		} 
		else {
			event.preventDefault();
		}
	});
	function callbackFunction (resp) {
		if (resp.result === "success") {
			formSuccessSub();
		}
		else {
			formErrorSub();
		}
	}
	function formSuccessSub(){
		$(".newsletter-form")[0].reset();
		submitMSGSub(true, "Thank you for subscribing!");
		setTimeout(function() {
			$("#validator-newsletter").addClass('hide');
		}, 4000)
	}
	function formErrorSub(){
		$(".newsletter-form").addClass("animated shake");
		setTimeout(function() {
			$(".newsletter-form").removeClass("animated shake");
		}, 1000)
	}
	


	// Nice Select JS
	$('select').niceSelect();

	// Odometer JS
	$('.odometer').appear(function(e) {
		var odo = $(".odometer");
		odo.each(function() {
			var countNumber = $(this).attr("data-count");
			$(this).html(countNumber);
		});
	});

	// Popup Video
	$('.popup-youtube').magnificPopup({
		disableOn: 320,
		type: 'iframe',
		mainClass: 'mfp-fade',
		removalDelay: 160,
		preloader: false,
		fixedContentPos: false
	});
	// WOW Animation JS
	if($('.wow').length){
		var wow = new WOW({
			mobile: false
		});
		wow.init();
	}

	// Go to Top
	$(window).on('scroll', function(){
		var scrolled = $(window).scrollTop();
		if (scrolled > 600) $('.go-top').addClass('active');
		if (scrolled < 600) $('.go-top').removeClass('active');
	});  
	$('.go-top').on('click', function() {
		$("html, body").animate({ scrollTop: "0" },  500);
	});
	
	// Preloader Area
	$(window).on('load', function() {
		$('.preloader').addClass('preloader-deactivate');
	});

}(jQuery));

// function to set a given theme/color-scheme
function setTheme(themeName) {
    localStorage.setItem('fido_theme', themeName);
    document.documentElement.className = themeName;
}

// Immediately invoked function to set the theme on initial load
(function () {
        setTheme('theme-light');
      document.getElementById('slider').checked = true;
})

();