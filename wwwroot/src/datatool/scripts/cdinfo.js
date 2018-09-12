function setLanguage(a) {
  //  document.aspnetForm.slng.value=a;
  //  document.aspnetForm.clng.value='t';

  //  document.aspnetForm.submit();
  //  document.aspnetForm.target = '_top';
    //  return true;

  var theForm = document.forms['aspnetForm'];
  theForm.slng.value = a;
  theForm.clng.value = 't';
  theForm.submit();
  theForm.target = '_top';
  return true;
}

function btnSubmit(inputText) {
    window.location = "https://www.canada.ca/en/sr.html?" + inputText;
}

function Email(sURL) {   //alert(sURL);
  w1 = window.open(sURL, 'EmailWin', 'height=320px width=500px resizable=no menubar=yes scrollbars=no');
  w1.focus();
}
function Link(sURL) {   //alert(sURL);
  w2 = window.open(sURL, 'LinkWin', 'height=600px width=500px resizable=no menubar=yes scrollbars=no');
  w2.focus();
}
function SelectCTL(sCTL) {
  sCTL.select();
}
function CreateIFrame(Str) {
  var div = document.createElement('div');
  var str = "<div id='emailframe' class=\"";
  str += "emailPopUp";
  str += "\"><iframe src='" + Str + "' frameborder=0 width=360px height=70px scrolling=no></iframe></div>";
  div.innerHTML = str;
  document.body.appendChild(div.firstChild);
}

//function SaveChart(a) {
//    //alert(a);
//    a.exportImage({ type: "image/jpeg" });
//    return false;
//}
//function PrintChart(a) {
//    a.print();
//    return false;
//}
//function CreateSFrame(Str,e) {
//    var frm = window.parent.document.getElementById('Surveyframe');
//    if (frm) { frm.parentNode.removeChild(frm); }
//    var posX, posY;
//    posX = 0;
//    posY = 0;
//    if (!e) var e = window.event;

//    var ctX = 0, ctY = 0;
//    if (window.innerHeight) {
//        posX = window.pageXOffset;
//        posY = window.pageYOffset;
//        ctX = e.pageX;
//        ctY = e.pageY-posY ;
//    }
//    else if (document.documentElement && document.documentElement.scrollTop) {
//        posX = document.documentElement.scrollLeft;
//        posY = document.documentElement.scrollTop;
//        ctX = e.clientX;
//        ctY = e.clientY;        
//    }
//    else if (document.body) {
//        posX = document.body.scrollLeft;
//        posY = document.body.scrollTop;
//        ctX = e.clientX;
//        ctY = e.clientY;
//    }

//    posY = posY + ctY + 20;
//    posX = posX + ctX - 400;

//    var DStyle = "style = \"";
//    DStyle += "border: 0px solid #3366CC; ";
//    DStyle += "z-index: 1; ";
//    DStyle += "position: absolute; ";
//    DStyle += "width: 500px; ";
//    DStyle += "background-color: #CEDFFF; "
//    DStyle += "font-family: Arial, Helvetica, sans-serif; ";
//    DStyle += "top: " + posY + "px; Left: " + posX + "px \"";

//    var mydiv = document.createElement('div');
//    var Divstr = "<div id='Surveyframe' ";
//    Divstr += DStyle;
//    Divstr += "><iframe src='" + Str + "' frameborder=0 width=500px height=320px scrolling=auto></iframe></div>";
//    mydiv.innerHTML = Divstr;
//    document.body.appendChild(mydiv.firstChild);
//}

//function autoResize(e) {
////    var newheight;
////    var newwidth;

////    if (document.getElementById) {
////        newheight = document.getElementById(id).contentWindow.document.body.scrollHeight;
////        newwidth = document.getElementById(id).contentWindow.document.body.scrollWidth;
////    }

////    document.getElementById(id).height = (newheight) + "px";
////    document.getElementById(id).width = (newwidth) + "px";

//    // Set width of iframe according to its content
//    e.Width = 500;
//    if (e.Document && e.Document.body.scrollHeight) //ie5+ syntax
//        e.Height = e.contentWindow.document.body.scrollHeight;
//    else if (e.contentDocument && e.contentDocument.body.scrollHeight) //ns6+ & opera syntax
//        e.Height = e.contentDocument.body.scrollHeight + 35;
//    else if(e.contentDocument && e.contentDocument.body.offsetHeight) //standards compliant syntax – ie8
//    e.Height = e.contentDocument.body.offsetHeight + 35;
//        else
//        e.Height=320;

//}
