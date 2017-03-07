function ToggleSelectAll(cbheader, gridid) {
    var docElements = document.forms[0];
    var l = docElements.elements.length;
    
    //Search ex. gridOrthographicSearch__ctl9_chkSelect

    for (i = 0; i < docElements.length; i++) {
        var name = docElements[i].id;
        if (name.indexOf(gridid) > -1 && name.indexOf('chkSelect') > -1) {
            docElements[i].checked = cbheader.checked;
        }
    }
}

function Swap(image, replace)
{
	if(document.images)
	{
		image.src = replace.src;
		return true;
	}
	else
		return false;
}

function DynamicWeights( firstvalue, secondvalue, thirdvalue )
{
	if ( firstvalue == '' || secondvalue == '' || thirdvalue == '' )
	{		
		alert("You must enter values in all boxes");
		return false;
	}
	
	alert("Hello");
	var fvalue = parseInt(firstvalue);
	var svalue = parseInt(secondvalue);
	var tvalue = parseInt(thirdvalue);
	var total = fvalue + svalue + tvalue; 

	if (total != 100)
	{
		alert("Values must equal 100 when combined.");
		return false;
	} else {
		return true;
	}

}

function ConfirmDelete()
{
	
	var agree=confirm("Are you sure you want to mark this item as complete?");
	if (agree)
		return true ;
	else
		return false ;

}

function ClearNewsItem()
{
	TxtNewsId.value = '';
	TxtHeadine.value = '';
	TxtTeaser.value = '';
	TXTExpire.value = '';
	TANews.value = '';
	DDLCategory.SelectedIndex(0);
	DDLPriority.SelectedIndex(0);
}


if(document.images)
{
	var submit1 = new Image();
	submit1.src = "Images/submit.jpg";
	
	var submit2 = new Image();
	submit2.src = "Images/submit2.jpg";

	var reset1 = new Image();
	reset1.src = "Images/reset.jpg";
	
	var reset2 = new Image();
	reset2.src = "Images/reset2.jpg";

	var login1 = new Image();
	login1.src = "Images/submit.jpg";
	
	var login2 = new Image();
	login2.src = "Images/submit2.jpg";

}