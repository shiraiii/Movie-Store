
$(document).on('click', '.nav-tab li ', function () {
	$(this).addClass('active').siblings().removeClass('active');
});

const fadeIn = document.querySelectorAll('.tab-movies-line .nav-tab li');
var tabMovie1 = document.querySelector('#tab-onshow');
var tabMovie2 = document.querySelector('#tab-oncoming');


function switchPane() {
	tabMovie1.classList.toggle('active')
	tabMovie2.classList.toggle('active')
}

for (const active of fadeIn) {
	active.addEventListener('click', switchPane)
}

