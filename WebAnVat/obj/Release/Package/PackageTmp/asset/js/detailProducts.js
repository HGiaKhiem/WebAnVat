// Trang chi tiết sản phẩm: tăng giảm sản phẩm
const counters = document.querySelectorAll(".counter")
counters.forEach(counter => {
    let number = 0
    const numberDisplay = counter.querySelector(".number")
    const btnMinus = counter.querySelector(".btnMinus")
    const btnPlus = counter.querySelector(".btnPlus")

    btnMinus.addEventListener("click", () => {
        number--
        if (number < 0) {
            number = 0
        }
        updateDisplay()
    })

    btnPlus.addEventListener("click", () => {
        number++
        updateDisplay()
    })

    function updateDisplay() {
        numberDisplay.textContent = number
    }
})

const btnSizes = document.querySelectorAll(".wrapDetail__btnSize")
btnSizes.forEach((btn, index) => {
    btn.addEventListener('click', () => {
        document.querySelector(".wrapDetail__btnSize.active").classList.remove('active')
        btn.classList.add('active')
    })
})


const options = document.querySelectorAll(".wrapDetail__wrapOption")
options.forEach((option, index) => {
    const btnOptions = option.querySelectorAll(".wrapDetail__btn")
    btnOptions.forEach((btn, index) => {
        btn.addEventListener("click", () => {
            option.querySelector(".wrapDetail__btn.active").classList.remove('active')
            btn.classList.add('active')
        })
    })
})