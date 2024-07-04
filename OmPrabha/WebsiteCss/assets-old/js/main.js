(function($){
    'use strict';

    $(document).ready(function(){
        var $win             =   $(window),
            $document        =   $(document),
            $body            =   $('body'),
            $header          =   $('header'),

            $productDesc     =   $("#product-description"),

            $scrollup        =    $('.scrollup'),
            $bannerslide     =    $('.banner-slide'),
            $featuredBlog    =    $('.featured-blog-list'),
            $upcomming       =    $('.up-comming-slide'),
            $related         =    $('.related-slide'),
            $dropdown        =    $('.dropdown'),
            $dropdownMenu    =    $('.dropdown-menu'),
            $gallery         =    $('.gallery'),
            $testi           =    $('.testi'),
            $blogBanner      =    $('.blog-banner'),
            $galleryFull     =    $('.gallery-full'),
            $grid            =    $('.grid'),
            $loginModal      =    $('.modal-login'),
            $registerModal   =    $('.modal-register'),
            $resetPass       =    $('.forget-btn');


        //////scroll function for changed css starts
        $win.scroll(function() {
            // shrink headers
            if ($(this).scrollTop() > 0) {
                $header.addClass("scrolled");
            } else {
                $header.removeClass("scrolled");
            }

            // back to top
            if ($(this).scrollTop() > 200) {
                $scrollup.fadeIn();
            } else {
                $scrollup.fadeOut();
            }
        });

        $scrollup.on("click", function() {
            $body.animate({
                scrollTop: 0
            }, 600);
            return false;
        });

        //banner slider
        $bannerslide.slick({
            dots: false,
            infinite: true,
            speed: 800,
            fade: true,
            cssEase: 'linear',
            autoplay: true,
            draggable: true,
            lazyLoad: "ondemand",
            pauseOnFocus: false
        });


        // prise slider
        if ($win.width() < 767) {
            $featuredBlog.bxSlider({
                minSlides: 1,
                maxSlides: 1,
                slideWidth: 370,
                slideMargin: 30,
                moveSlides: 1,
                auto: true,
                pager: false,
                nextText: '<i class="fa fa-long-arrow-right">',
                prevText: '<i class="fa fa-long-arrow-left">'
            });
        } else {
            $featuredBlog.bxSlider({
                minSlides: 2,
                maxSlides: 3,
                slideWidth: 370,
                slideMargin: 30,
                moveSlides: 1,
                auto: true,
                pager: false,
                nextText: '<i class="fa fa-long-arrow-right">',
                prevText: '<i class="fa fa-long-arrow-left">'
            });
        }


        // up-comming-list
        $upcomming .bxSlider({
            auto: true,
            pager: false
        });


        // related slider 
        $related.slick({
            infinite: true,
            slidesToShow: 3,
            slidesToScroll: 1,
            autoplay: true,
            draggable: true,
            responsive: [{
                breakpoint: 767,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }]
        });

        // drop down menu in mobile
        $dropdown.on('click', 'a', function(e) {
            var $this = $(this);
            var collapseThis = false;
            if ($dropdownMenu.hasClass('dropdown-open') &&
                !$this.next('.dropdown-menu').hasClass('dropdown-open')) {
                $dropdownMenu.slideUp().removeClass('dropdown-open');
                collapseThis = false;
            } else if ($(this).next('.dropdown-menu').hasClass('dropdown-open')) {
                collapseThis = true;
                $(this).next('.dropdown-menu').slideUp().removeClass('dropdown-open');
            }

            if (!collapseThis) {
                $(this).next('.dropdown-menu').slideDown().addClass('dropdown-open');
            }
        });

        // product description     
        $productDesc.accordion({
            heightStyle: "content"
        });

        //thumb slider
        $gallery.bxSlider({
            pagerCustom: '.gallery-pager',
            mode: "fade",
            auto: true,
            nextText: '<i class="fa fa-angle-right">',
            prevText: '<i class="fa fa-angle-left">'
        });

         //blog slider
        $blogBanner.bxSlider({ 
            mode: "fade",
            auto: true
        });

        //property detail full slider
        $galleryFull.bxSlider({
            mode: "fade",
            auto: true
        });


        // mesonary blog
        $grid.masonry({
            itemSelector: '.grid-item'
        });


        //price range
        var $amount = $('#amount'),
            $amount1 = $('#amount1'),
            $amount2 = $('#amount2'),
            $rangeSlider = $('#slider-range');

            $rangeSlider.slider({
                range: true,
                min: 0,
                max: 500,
                values: [30, 400],
                slide: function(event, ui) {
                    $amount.html("<span>$" + ui.values[0] + "</span>  <span>$" + ui.values[1] + "</span>");
                    $amount1.val(ui.values[0]);
                    $amount2.val(ui.values[1]);
                }
            });

        $amount.html("<span>$" + $rangeSlider.slider("values", 0) +
            "</span>  <span>$" + $rangeSlider.slider("values", 1) + "</span>");


        //price range
        var $area = $('#area'),
            $area1 = $('#area1'),
            $area2 = $('#area2'),
            $areaRange = $('#area-range');

            $areaRange.slider({
                range: true,
                min: 0,
                max: 10000,
                values: [150, 5000],
                slide: function(event, ui) {
                    $area.html("<span> Sqft" + ui.values[0] + "</span>     <span> Sqft " + ui.values[1] + "</span>");
                    $area1.val(ui.values[0]);
                    $area2.val(ui.values[1]);
                }
            });

        $area.html("<span> Sqft " + $areaRange.slider("values", 0) +
            "</span> <span> Sqft " + $areaRange.slider("values", 1) + "</span>");


        // testimonail sider
        $testi.bxSlider({
            pagerCustom: '.testimonail-thumb',
            auto: true
        });

        // light box 
        $document.on('click', '[data-toggle="lightbox"]', function(event) {
            var $this = $(this);
            event.preventDefault();
            $this.ekkoLightbox();
        });

        // login modal
        $loginModal.click(function() {
            $(".login-tab").addClass('active');
            $(".register-tab").removeClass('active');
        });

        // register modal
        $registerModal.click(function() {
            $(".register-tab").addClass('active');
            $(".login-tab").removeClass('active');
        });

        // recover modal
        $resetPass.click(function() {
            $(".login-register").addClass('hide');
            $(".recover ").removeClass('hide');
        });

        // file input
        var inputs = document.querySelectorAll('.inputfile');
        Array.prototype.forEach.call(inputs, function(input) {
            var label = input.nextElementSibling,
                labelVal = label.innerHTML;

            input.addEventListener('change', function(e) {
                var fileName = '';
                if (this.files && this.files.length > 1)
                    fileName = (this.getAttribute('data-multiple-caption') || '').replace('{count}', this.files.length);
                else
                    fileName = e.target.value.split('\\').pop();

                if (fileName)
                    label.querySelector('span').innerHTML = fileName;
                else
                    label.innerHTML = labelVal;
            });

            // Firefox bug fix
            input.addEventListener('focus', function() { input.classList.add('has-focus'); });
            input.addEventListener('blur', function() { input.classList.remove('has-focus'); });
        });
    });


})(jQuery);
