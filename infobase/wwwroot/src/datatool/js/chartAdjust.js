     
            // colorRef references the default dotNETCharting bar colors for selection purposes.
            var colorRef = ["rgb(106,19,36)", "rgb(111,115,12)", "rgb(60,105,44)"];
            var colorRefLines = ["rgb(186,192,20)", "rgb(178,33,61)", "rgb(100,175,74)"];
            var colorRefHIDT = ["rgb(194,238,141)", "rgb(160,171,203)", "rgb(232,221,174)"];
            var colorRefStopColor = ["rgb(219,181,174)", "rgb(224,230,161)", "rgb(164,222,133)"];
        var labelRef = [];

        // Selects all labels from the legend.
        function getLabels() {
            setLineColor();
            setLegendSize();
            rotateTitle();
            var legendLabels = $('g[class*="legend-item"]');
            // Loops through the labels and adds to the labelRef array.
            for (var i = 0; i < legendLabels.length; i++)
                labelRef.push($(legendLabels).eq(i).find('text').first().text());

            colorRef = colorRef.slice(0, labelRef.length);

            var tabText = $('ul[class="nav nav-tabs list-inline"] a[href*="javascript"]').text();

            //colorRef = colorRef.reverse();
            // Loops through the labelRefs and adds the textures by label.
            for (var i = 0; i < labelRef.length; i++)
                addTextures(i);
        }


        // Changes the color of the borders for the graph bars (mouseover and mouseout), the graph margin of error lines, and the legend items. Also, the tooltip style is removed.
        function setLineColor() {
            $('g[class$="grid"]').first().before($('g[class$="series-group"]'));
            $('g[class*="tracker"]').on('mouseout', function () {
                $(this).find('rect').attr('stroke', '#686868');
            });
            $('g[class*="tracker"]').on('mouseover', function () {
                $(this).find('rect').attr('stroke', '#686868');
                // Change tooltip on mouseover of the graph bars.
                if (!$('#tooltipPath').length) {
                    $('g[class$="tooltip"] path[fill^="url(#JSC_"]').attr('id', 'tooltipPath');
                }
                var copyTooltip = $('#tooltipPath').clone();
                $('#tooltipPath').remove();
                $(copyTooltip).attr('stroke', $(this).find('rect').first().attr('fill'));
                $(copyTooltip).attr('stroke-width', 3);
                (copyTooltip).attr('fill', "#fff");
                $(copyTooltip).attr('d', $('g[class$="tooltip"] path').first().attr('d'));
                $('g[class$="tooltip"] path').last().after($(copyTooltip));
            });
            $('g[class*="tracker"]').find('rect').attr('stroke', '#686868');
            $('g[class$="series-group"] g[class$="series"]').find('path').attr('stroke', '#686868');
            $('g[class*="legend-item"]').find('rect').attr('stroke', '#686868');
        }

        // Resizes the legend <rect> items.
        function setLegendSize() {
            $('g[class*="legend-item"] rect').attr('height', 19);
            $('g[class*="legend-item"] text').attr('y', 14.5);
        }

        function addTextures(count) {
var barExists = true;
	// barColorID and labelColorID refer to the fill color references of the graph bar and legend <rect>, the fill color is always in the format "url(#{color id})".
	if (typeof($('g[class*="tracker"]').eq(count).find('rect').first().attr('fill')) === "undefined")
		barExists = false;
	var barColorID;
	var labelColorID ;
	
	if (barExists){
		barColorID = $('g[class*="tracker"]').eq(count).find('rect').first().attr('fill').slice(5, -1);
	
	
		labelColorID = $('g[class*="legend-item"]').eq(count).find('rect').first().attr('fill').slice(5, -1);
	}
	
		
            // barColorID and labelColorID refer to the fill color references of the graph bar and legend <rect>, the fill color is always in the format "url(#{color id})".
        //    var barColorID = $('g[class*="tracker"]').eq(count).find('rect').first().attr('fill').slice(5, -1);
        //    var labelColorID = $('g[class*="legend-item"]').eq(count).find('rect').first().attr('fill').slice(5, -1);
            // Generate the texture object and ID based on the current label. Both sexes (blue), Males (green with texture), and Females (yellow).
        
		var t = textures.lines()
                .strokeWidth(1)
                .background("#3BB15D");
            if (labelRef[count].includes("Both sexes") || labelRef[count].includes("Hommes et femmes")) {
                t = textures.lines()
                .strokeWidth(0)
                .background("#4C91F6");
            }
            else if (labelRef[count].includes("Females") || labelRef[count].includes("Femmes") || labelRef[count].includes("With ") || labelRef[count].includes("Avec ")) {
                t = textures.lines()
                .strokeWidth(0)
                .background("#fbc30a");
            }
            //else {
             //   t = textures.lines()
            //    .strokeWidth(1)
             //   .background("#3BB15D");
            //}

            d3.select('#svgTexture').call(t);

            // Copy and replace the bar and legend color references.
            if (barExists)
			{
			var copyLabel = $('#' + t.id()).clone();
	var copyBar = $('#' + t.id()).clone();
	var copyHover = $('#' + t.id()).clone();
	$('#' + labelColorID).remove();
	//if (barExists)
		$('#' + barColorID).remove();
	$(copyLabel).attr('id', labelColorID);
	//if (barExists)
		$(copyBar).attr('id', barColorID);
	$('defs').first().append(copyLabel);
	$('defs').first().append(copyBar);
			}
			
    //        var copyLabel = $('#' + t.id()).clone();
     //       var copyBar = $('#' + t.id()).clone();
     //       var copyHover = $('#' + t.id()).clone();
     //       $('#' + labelColorID).remove();
     //       $('#' + barColorID).remove();
    //        $(copyLabel).attr('id', labelColorID);
    //        $(copyBar).attr('id', barColorID);
    //        $('defs').first().append(copyLabel);
    //        $('defs').first().append(copyBar);

            // Replace the mouseover event fill color when a graph bar triggers a mouseover event (only the first time).
            $('g[class*="tracker"]').eq(count).on('mouseover', function () {
                var hoverColorID = $('stop[stop-color="' + colorRef[count] + '"]').first().parent().attr('id');
                //console.log(hoverColorID);
                if ($('#' + hoverColorID).length) {
                        $('#' + hoverColorID).remove();
                        $(copyHover).attr('id', hoverColorID);
                        $('defs').first().append(copyHover);
                }
            });
        }

        function rotateTitle() {
            var axisRef = [];
            // get Axis lable;
            var axisLabels = $('g[class*="axis"]');
            for (var i = 0; i < axisLabels.length; i++) {
                axisRef.push($(axisLabels).eq(i).find('text').first().text());

                if (axisRef[i].includes("Ratio")) {
                    var x1 = $(axisLabels).eq(i).find('text').first().attr('x');
                    x1 = Number(x1) + 10;
                    var y1 = $(axisLabels).eq(i).find('text').first().attr('y');
                    var r1 = "translate(0,0) rotate(270 " + x1 + " " + y1 + ")";
                    $(axisLabels.eq(i).find('text')).attr('transform', r1);
                    return false;
                }
            }
        }

		
        // This code only executes once the SVG is added to DOM.
        $(window).load(function () {
            //Don't want this to be executed on map page, check that tab is not selected
            var tabText = $('ul[class="nav nav-tabs list-inline"] a[href*="javascript"]').text();
            if ((tabText.includes("Compar"))) {
                var i = setInterval(function () {
                    if ($('svg').length) {
                        // Change SVG id for jQuery selection.
                        $('svg').first().attr('id', 'svgTexture');
                        clearInterval(i);
                        getLabels();
                        $(window).resize(function () {
                            setLegendSize();
                            rotateTitle();
                        });
                    }
                }, 100);
            }
        });
		
		
		
    