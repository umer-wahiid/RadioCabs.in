let slideIndex = 1;
showSlide(slideIndex);

function prevSlide() {
    showSlide((slideIndex -= 1));
}

function nextSlide() {
    showSlide((slideIndex += 1));
}

function showSlide(index) {
    const slides = document.getElementsByClassName("slide");

    if (index < 1) {
        slideIndex = slides.length;
    } else if (index > slides.length) {
        slideIndex = 1;
    }

    for (let i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }

    slides[slideIndex - 1].style.display = "block";
}
