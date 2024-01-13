$(document).on('click', '.nav-tab li  ', function(){	
	$(this).addClass('active').siblings().removeClass('active');
});


$(document).on('click', ' .tabs label	', function(){	
	$(this).addClass('active').siblings('').removeClass('active');
});


function setDatepicker(_this) {
  
	/* Get the parent class name so we 
		can show date picker */
	let className = $(_this).parent()
		.parent().parent().attr('class');

	// Remove space and add '.'
	let removeSpace = className.replace(' ', '.');

	// jQuery class selector
	$("." + removeSpace).datepicker({
		format: "dd/mm/yyyy",

		// Positioning where the calendar is placed
		orientation: "bottom auto",
		// Calendar closes when cursor is 
		// clicked outside the calendar
		autoclose: true,
		showOnFocus: "false"
	});
}

$(document).ready(function(){
    $("#loginBuyticket").click(function(){
        $("#myToast").toast("show");
    });
});

const fadeIn = document.querySelectorAll('.tab-movies .nav-tab li');
const loginFade = document.querySelectorAll('.tab-login-line .nav-tab li');
const buyFade = document.querySelectorAll('.tabs label');
const tabPane1 = document.querySelector('#tab-default-1');
const tabPane2 = document.querySelector('#tab-default-2');
const tabLog1 = document.querySelector('#tab-login-1');
const tabLog2 = document.querySelector('#tab-login-2');
const logIn = document.querySelector('.js-login-section');
const modal = document.querySelector('.js-modal');
const modalContainer = document.querySelector('.js-modal-container');
const modalClose = document.querySelector('.js-modal-close');
const buyTab1 = document.querySelector('#content-1');
const buyTab2 = document.querySelector('#content-2');
const buyTab3 = document.querySelector('#content-3');
const label1 = document.querySelector('.tab-label-1');
const label2 = document.querySelector('.tab-label-2');
const label3 = document.querySelector('.tab-label-3');

function switchPane(){
	tabPane1.classList.toggle('active');
	tabPane2.classList.toggle('active');	
}

function switchMenu(){
	tabLog1.classList.toggle('active');
	tabLog2.classList.toggle('active');
}

label2.addEventListener('click', () => {
	buyTab2.classList.add('active');
	buyTab1.classList.remove('active');
	buyTab3.classList.remove('active');
});

label1.addEventListener('click',() =>{
	buyTab2.classList.remove('active');
	buyTab1.classList.add('active');
	buyTab3.classList.remove('active');
});

label3.addEventListener('click',() =>{
	buyTab2.classList.remove('active');
	buyTab1.classList.remove('active');
	buyTab3.classList.add('active');
});



for(const active of fadeIn){
	active.addEventListener('click', switchPane)
}

for(const changeMenu of loginFade){
	changeMenu.addEventListener('click', switchMenu)
}

/*function showLogIn(){
	modal.classList.add('open')
}

function hiddenLongIn(){
	modal.classList.remove('open')
}

logIn.addEventListener('click', showLogIn)


modalClose.addEventListener('click', hiddenLongIn)
*/

var counter = 1;
setInterval(function(){
  document.getElementById('radio' + counter).checked = true;
  counter++;
  if(counter > 4){
	counter = 1;
  }
}, 5000);

// modalContainer.addEventListener('click', function(event){
// 	event.stopPropagation();
// })

