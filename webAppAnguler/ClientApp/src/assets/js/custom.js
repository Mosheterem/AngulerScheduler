// Owl banner slider


$('.video_slider').owlCarousel({
    loop                    : true,
    margin                  : 0,
    nav                     : true,
    navText                 :['<img src="images/leftarrow.png" alt=""/>','<img src="images/rightarrow.png" alt=""/>'],
    autoplay                : false,
	video                   : true,
	
    dots                    : false,    
    touchDrag               : false,
    mouseDrag               : true,
    responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },
        1000:{
            items:1
        }
		
    },
	 onTranslate: function() {
        $('.owl-item').find('video').each(function() {
            this.pause();
			  $(".video_button").css('visibility', 'visible');
			      $(".vid_img").css('visibility', 'visible');
        });
    }
	
})



// Menu Toggle
$(".toogle_btn").click(function(){
	$(".menu nav").slideToggle("fast")
	$("body").toggleClass("overlay")
});


$('.video_button').click(function () {
    var video = $(this).closest('.video_box').find('video')[0];
    video.play();
    $(".vid_img").css('visibility', 'hidden');
    $(this).css('visibility', 'hidden');
    return false;
});



$(function () {
  $('[data-toggle="tooltip"]').tooltip()
})

$(function(){
	$('input[type="number"]').niceNumber();
});


$('.date').datepicker({
  //multidate: true,
	format: 'dd-mm-yyyy'
});



$(document).ready(function(){
    $('input.timepicker').timepicker({});
});


