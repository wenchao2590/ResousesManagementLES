<!DOCTYPE html>  
<html lang="en">  
  <head>  
	<meta charset="utf-8">  
	<title>jQRangeSlider demo page</title>  

	<link rel="stylesheet" id="themeCSS" href="../css/iThing.css"> 
	<link rel="stylesheet" href="style.css">
	
	<script src="../lib/jquery-1.10.2.min.js"></script>
	<script src="../lib/jquery-ui.min.js"></script>
	<script src="../lib/jquery.mousewheel.min.js"></script>
	
	<!-- Debug -->
	<script src="../jQRangeSliderMouseTouch.js"></script>
	<script src="../jQRangeSliderDraggable.js"></script>
	<script src="../jQRangeSliderHandle.js"></script>
	<script src="../jQRangeSliderBar.js"></script>
	<script src="../jQRangeSliderLabel.js"></script>
	<script src="../jQRangeSlider.js"></script>

	<script src="../jQDateRangeSliderHandle.js"></script>
	<script src="../jQDateRangeSlider.js"></script>
	
	<script src="../jQEditRangeSliderLabel.js"></script>
	<script src="../jQEditRangeSlider.js"></script>
	<!-- /Debug -->
	
	<!-- Minified --><!--
	<script src="../jQAllRangeSliders-min.js"></script>
	--><!-- /Minified -->

	<script src="sliderDemo.js"></script>
	<script src="dateSliderDemo.js"></script>
	<script src="editSliderDemo.js"></script>

	
  </head>  
  <body>
  	<h1>jQRangeSlider demos</h1>

  	<form onsubmit="return false">
  	

<div id="shanks" class=""></div>
</form>




<Script>

(function($, undefined){

	function createDemos(){
		var simple = $("<div id='slider' />").appendTo("body"),
			date = $("<div id='date' />").appendTo("body"),
			modifiable = $("<div id='modifiable' />").appendTo("body");

	//	simple.sliderDemo();
	//	date.dateSliderDemo();
	///	modifiable.editSliderDemo();
    $("#shanks").rangeSlider({
            bounds:{min:0,max:1110}
        });
    
	}

	function changeTheme(e){
		var target = $(e.currentTarget),
			path = "../css/",
			theme;

		if (target.hasClass("selected")){
			return
		}

		$("#themeSelector .selected").removeClass("selected");

		theme = target.attr("class");

		$("#themeSelector ."+theme).addClass("selected");

		$("#themeCSS").attr("href", path + theme + ".css");

		setTimeout(function(){
			$(window).resize();
		}, 500);
	}

	function initTheme(){
		$("#themeSelector dd, #themeSelector dt").click(changeTheme);
	}

	$(document).ready(function(){
		createDemos();
		initTheme();
	});

})(jQuery);
</script>
	</body>  
</html>  