var possibleResults = [];
var sortType = 2;

var french = false;
var frenchVal = "";
var currentPage = "";
var viewText = "";

$(document).ready(function () {

	var firstSlash = window.location.pathname.indexOf('/');
	var secondSlash = window.location.pathname.indexOf('/', 1);
	currentPage = window.location.pathname.substring(firstSlash + 1, secondSlash);
	
	var currentURL = window.location.href;
	if(currentURL.match(/l=fra/)||currentURL.match(/index-fr/)){
		french = true;
		frenchVal = "l=fra";
	}
	
	if (french != true) {
		$.get("../src/json/webCrawlerResults.json", jsonDoc, "json");
		console.log("hello");
		viewText = "View";
		tagText = "Tags:";
	} else {
		$.get("../src/json/webCrawlerResultsFR.json", jsonDoc, "json");
		console.log("allo");
		viewText = "Afficher";
		tagText = "Tags :";
	}
	
	var queryString = window.location.href.split("?")[1];
	if (typeof(queryString) !== "undefined") {
		queryString = queryString.split("&");
		var searchType = -1;
		var searchText = "";
		for (var i = 0; i < queryString.length; i++) {
			if (queryString[i].split("=")[0] === "search")
				searchText = unescape(queryString[i].split("=")[1]);
			else if (queryString[i].split("=")[0] === "sortType")
				searchType = parseInt(queryString[i].split("=")[1]);
		}
		if (searchText !== "" && searchType != -1) {
			$('#filter').val(searchText);
			search(searchType);
		}
	}

	$('#search').click(function () {
		console.log("searching...");
		search(sortType);
	});

	$('#filter').keydown(function (event) {
		if (event.keyCode == 13) {
			console.log("searching...");
			search(sortType);
		}
	});

	$('.sortType').click(function () {
		sortType = $(this).val();
		$(".btn-success").toggleClass("btn-success").toggleClass("btn-primary");
		$(this).toggleClass("btn-primary").toggleClass("btn-success");
		search(sortType);
	});
});

function jsonDoc(data) {
	var i = "";
	console.log(data);
	$.each(data.results, function (k, v) {
		if (v.c == "db") {
			imgSrc = v.i;
				if(french == false) {
				imgAlt = "Data Blog";
				tagLink = "http://infobase.phac-aspc.gc.ca/datalab/blog-en.html";
				}else{
				imgAlt = "Blogue de données";	
				tagLink = "http://infobase.phac-aspc.gc.ca/datalab/blog-fr.html";
				}
			tag = imgAlt;			 
		} else if (v.c == "dv") {
			imgSrc = v.i;
				if(french == false) {
				imgAlt = "Data Visualization";
				tagLink = "http://infobase.phac-aspc.gc.ca/datalab/visualize-en.html";
				}else{
				imgAlt = "Visualisation de données";
				tagLink = "http://infobase.phac-aspc.gc.ca/datalab/visualize-fr.html";
				}
			tag = imgAlt;
		} else if (v.c == "dc") {
			imgSrc = currentPage == "data-tools" ? "http://infobase.phac-aspc.gc.ca/canada/img/DataCubeIcon.png"  : "/src/img/DataCubeIcon.png";
				if(french == false) {
				imgAlt = "Data Cubes";
				tag = "Data Tools";
				tagLink = "http://infobase.phac-aspc.gc.ca/data-tools/";
				}else{
				imgAlt = "Cubes de données";	
				tag = "Outils de données";
				tagLink = "http://infobase.phac-aspc.gc.ca/data-tools/?l=fra";
				}
		} else if (v.c == "ccdi") {
			imgSrc = "/src/img/CCDI.png";
			if(french == false) {
			imgAlt = "Canadian Chronic Disease Indicators";
			tag = "CCDI";
			tagLink = "http://infobase.phac-aspc.gc.ca/ccdi-imcc/";
			}else{
			imgAlt = "Indicateurs des maladies chroniques au Canada";
			tag = "IMCC";
			tagLink = "http://infobase.phac-aspc.gc.ca/ccdi-imcc/index-fr.aspx";
			}
		} else if (v.c == "pmh") {
			imgSrc = "/src/img/PMHSIF.png";
			if(french == false) {
			imgAlt = "Positive Mental Health Surveillance Indicator Framework";
			tag = "PMHSIF";
			tagLink = "http://infobase.phac-aspc.gc.ca/positive-mental-health/";
			}else{
			imgAlt = "Cadre d’indicateurs de surveillance de la santé mentale positive";
			tag = "CISSMP";
			tagLink = "http://infobase.phac-aspc.gc.ca/positive-mental-health/index-fr.aspx";
			}
		} else if (v.c == "pass") {
			imgSrc = "/src/img/PASS.png";
			if(french == false) {
			imgAlt = "Physical Activity, Sedentary Behaviour and Sleep Indicators";
			tag = "PASS";
			tagLink = "http://infobase.phac-aspc.gc.ca/pass-apcss/";
			}else{
			imgAlt = "Indicateurs de l’activité physique, du comportement sédentaire et du sommeil";
			tag = "APCSS";
			tagLink = "http://infobase.phac-aspc.gc.ca/pass-apcss/index-fr.aspx";
			}
		} else if (v.c == "ccdss") {
			imgSrc = "/src/img/CCDSSIcon.png";
			if(french == false) {
			imgAlt = "Canadian Chronic Disease Surveillance System";
			tag = "Data Tools";
			tagLink = "http://infobase.phac-aspc.gc.ca/ccdss-scsmc/data-tool/";
			}else{
			imgAlt = "Système canadien de surveillance des maladies chroniques";
			tag = "Outils de données";
			tagLink = "http://infobase.phac-aspc.gc.ca/CCDSS-SCSMC/data-tool/?l=fra";
			}
		} else if (v.c == "hi") {
			imgSrc = "/src/img/hi_square.png";
			if(french == false) {
			imgAlt = "Health Inequalities Data Tool";
			tag = "Health Inequalities";
			tagLink = "http://infobase.phac-aspc.gc.ca/health-inequalities/";
			}else{
			imgAlt = "Outil des données sur les inégalités en santé";
			tag = "Inégalités en santé";
			tagLink = "http://infobase.phac-aspc.gc.ca/health-inequalities/index-fr.aspx";
			}
		}

			var desc = v.d;
			if (desc.length > 330)
				desc = desc.substring(0, 330) + '...';
			var result = '<section><figure class="thumbnail post hght-inhrt"><a href="' + v.l + '"><img src="' + imgSrc + '"alt="' + imgAlt + '"></a><figcaption class="caption"><h4>' + v.h + '</h4><p><small><strong>' + tagText + '</strong></small> <a href="' + tagLink + '" class="resultTag">' + tag + '</a></p><p class="descText">' + desc + '</p><p><a href="' + v.l + '" class="btn btn-primary btnPosition">' + viewText + '</a></p></figcaption></figure></section>';
			 console.log(currentPage);
			if (currentPage == "datalab" && (v.c == "db" || v.c == "dv")){
				possibleResults.push(result);
			} else if (currentPage == "data-tools" && (v.c == "dc" || v.c == "ccdss" || v.c == "hi")) {
				possibleResults.push(result);
			} else if (currentPage == "indicators" && (v.c == "ccdi" || v.c == "pmh" || v.c == "pass")) {
				possibleResults.push(result);
			} else if (currentPage == "/") {
				possibleResults.push(result);
			} 
			
	});

	if ($('#filter').val() !== '') {
		search(sortType);
	}
}

function search(sortType) {
	var searchResults = [];
	var maxSearchScore = 0;

	//console.log("Possible Results Length: "+possibleResults.length);

	// Retrieve the input field text and reset the count to zero
	var filter = $('#filter').val(), count = 0;
	filter = filter.replace(/data\s*blog/ig, 'blog'); //if 'data blog' is entered, ignore 'data'
	filter = filter.replace(/data\s*visualization/ig, 'visualization'); //if 'data visualization' is entered, ignore 'data'
	filter = filter.replace(/infographic/ig, 'visualization'); //infographic is equivalent to visualization

	filter = filter.replace(/data\s*cube/ig, 'cube'); //if 'data cube' is entered, ignore 'data'
	if (filter.match(/Positive\s*Mental\s*Health\s*Surveillance\s*Indicator\s*Framework\s*/, "gi") == null) {
		filter = filter.replace(/(Chronic\s*Disease*\s*and\s*Injury\s*)?Indicator\s*Framework/ig, ' ccdi ');
	}
	filter = filter.replace(/Positive\s*Mental\s*Health\s*Surveillance\s*Indicator\s*Framework\s*/ig, ' pmh ');
	//console.log(filter);
	var filterArr = filter.split(' ');

	//console.log("Filter Arr Length: "+filterArr.length);

	// Loop through the comment list
	$.each(possibleResults, function (ind, res) {
		var found = false;
		var searchScore = 0;
		var order = 0;
		var titleScore = 0;
		var test = 0;

		for (var i = 0; i < filterArr.length; i++) {
			if (filterArr[i].length > 2) {
				var resHeader = $(res).find('h4').first().text();
				var resDesc = $(res).find('.descText').first().text();
				var type = $(res).find('.resultTag').first().text();
				var az = resHeader.substring(0, 5);

				titleScore = (resHeader.match(new RegExp(filterArr[i], "gi")) || []).length;
				searchScore += titleScore > 0 ? titleScore + 40 : titleScore;
								
				searchScore += (resDesc.match(new RegExp(filterArr[i], "gi")) || []).length;
				searchScore += (type.match(new RegExp(filterArr[i], "gi")) || []).length;

				if (searchScore > 0) {
					found = true;
				}

				if (searchScore > maxSearchScore) {
					maxSearchScore = searchScore;
				}
			}
		}

		if (found) {
			var innerArr = [];
			innerArr.push(searchScore);
			innerArr.push(res);
			innerArr.push(type);
			innerArr.push(order);
			innerArr.push(az);
			searchResults.push(innerArr);
			count++;
		}
	});

	var maxType = "";

	for (var i = 0; i < searchResults.length; i++) {
		if (searchResults[i][0] == maxSearchScore) {
			maxType = searchResults[i][2];
		}
	}

	for (var i = 0; i < searchResults.length; i++){
			if (maxType == "Data Blog") {
				if (searchResults[i][2] == "Data Blog") {
					searchResults[i][3] = 1;
				} else if (searchResults[i][2] == "Data Visualization") {
					searchResults[i][3] = 2;
				} else if ((searchResults[i][2] == "ccdi") || (searchResults[i][2] == "pmh") || (searchResults[i][2] == "pass")) {
					searchResults[i][3] = 3;
				} else {
					searchResults[i][3] = 4;
				}
			} else if (maxType == "Data Visualization") {
				if (searchResults[i][2] == "Data Visualization") {
					searchResults[i][3] = 1;
				} else if (searchResults[i][2] == "Data Blog") {
					searchResults[i][3] = 2;
				} else if ((searchResults[i][2] == "ccdi") || (searchResults[i][2] == "pmh") || (searchResults[i][2] == "pass")) {
					searchResults[i][3] = 3;
				} else {
					searchResults[i][3] = 4;
				}
			} else if ((maxType == "ccdi") || (maxType == "pmh") || (maxType == "hi") || (maxType == "pass")) {
				//console.log("maxType trigger");
				if ((searchResults[i][2] == "ccdi") || (searchResults[i][2] == "pmh") || (searchResults[i][2] == "pass")) {
					searchResults[i][3] = 1;
				} else if (searchResults[i][2] == "Data Blog") {
					searchResults[i][3] = 2;
				} else if (searchResults[i][2] == "Data Visualization") {
					searchResults[i][3] = 3;
				} else {
					searchResults[i][3] = 4;
				}
			} else {
				if (searchResults[i][2] == "Data Cube") {
					searchResults[i][3] = 1;
				} else if ((searchResults[i][2] == "ccdi") || (searchResults[i][2] == "pmh") || (searchResults[i][2] == "pass")) {
					searchResults[i][3] = 2;
				} else if (searchResults[i][2] == "Data Blog") {
					searchResults[i][3] = 3;
				} else {
					searchResults[i][3] = 4;
				}
			}
		}

	console.log(searchResults);
	
	var text = "";
	$('#searchResult').html("");

	sort(searchResults, sortType);

	var colCount = 1;
	var sectionCount = 0;
	var previousCategory = "";
	
	if(french == false) 
	text = "<h2>Search Results</h2><p><span id='filter-count'></span></p>";
	else
	text = "<h2>Résultats de recherche</h2><p><span id='filter-count'></span></p>";	

	for (var i = 0; i < searchResults.length; i++) {
		var nextCategory = $(searchResults[i][1]).find('.resultTag').first().text();
		if (colCount == 4) {
			colCount = 1;
		}
		if (sortType == 2) {
			if (nextCategory !== previousCategory) {
				if (colCount != 1)
					text += "</div>";
				text += "<hr/><h3>" + nextCategory + "</h3>";
				previousCategory = nextCategory;
				colCount = 1;
			}
		}
		if (colCount == 1)
			text += $('#searchResult').html() + "<div class='row wb-eqht'>";
		text += $('#searchResult').html() + "<div class='col-md-4'>" + $(searchResults[i][1]).html() + "</div>";
		if (i == searchResults.length - 1 || colCount == 3)
			text += $('#searchResult').html() + "</div>";
		colCount++;
	}

	$('#searchResult').html(text);
	if ($('#filter').val() !== "" && $('#filter').val().length > 1 && searchResults.length > 0) {		
		window.history.pushState('searchQuery', 'Title', window.location.href.split("?")[0] + '?' + frenchVal + '&search=' + escape($('#filter').val()) + '&sortType=' + sortType);
		$('#noResult').hide();
		$('#index').hide('slow');
		$('#searchResult').fadeIn('slow');
	}
	else {
		window.history.pushState('searchQuery', 'Title', window.location.href.split("?")[0] + '?' + frenchVal);
		$('#index').fadeIn();
		$('#noResult').fadeIn('slow');
		$('#searchResult').hide();
	}

	// Update the result count
	if (count > 0 && $('#filter').val() !== "") {
		if(french == false)
		$("#filter-count").text("Results returned (" + count + ")");
		else
		$("#filter-count").text("Résultats retournés (" + count + ")");
	}else if ($('#filter').val() === "") {
		$("#filter-count").html('');		
	}else{
		if(french == false)
		$("#noResult").html("<strong>Nothing found!</strong>");
		else
		$("#noResult").html("<strong>Aucun résultat!</strong>");
	}
}

function sort(searchResults, sortType) {
	//Default sort behaviour

	//	console.log("The sort type is " + sortType);

	searchResults.sort(function (a, b) {
		var q1 = a[3];
		var q2 = b[3];
		var o1 = b[2];
		var o2 = a[2];

		var p1 = b[0];
		var p2 = a[0];

		var az1 = a[4];
		var az2 = b[4];

		if (sortType == 3) { //Relevance
			if (p1 < p2) return -1;
			if (p1 > p2) return 1;

			if (o1 < o2) return -1;
			if (o1 > o2) return 1;
		}
		else if (sortType == 2) { //Category
		
			if (q1 < q2) return -1;
			if (q1 > q2) return 1;

			if (o1 < o2) return -1;
			if (o1 > o2) return 1;

			if (p1 < p2) return -1;
			if (p1 > p2) return 1;
		}
		else {
			if (az1 < az2) return -1;
			if (az1 > az2) return 1;
		}

		return 0;
	});
}