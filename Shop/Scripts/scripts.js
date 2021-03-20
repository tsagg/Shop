try {
const tableInit = () => {
    const options = {
        valueNames: ["search-value"],
        indexAsync: true,
        page: 5,
        pagination: [{
            innerWindow: 1,
        }],
    };

    const table = new List("content-table", options);

    const event = new Event('click', {
        bubbles: true
    });

    const searchInput = document.querySelector('.page-nav');
    const pageBtn = document.querySelector('.page-btn');

    let allPages;
    if (Math.floor(table.items.length % table.page == 0)) {
        allPages = Math.floor(table.items.length / table.page);
    } else {
        allPages = Math.floor(table.items.length / table.page) + 1;
    }

    searchInput.setAttribute('placeholder', allPages);

    searchInput.addEventListener('input', () => {
        let elValue = searchInput.value.trim();
        const regPattern = /^\d+$/;
        const isNumber = regPattern.test(elValue);

        if (!isNumber || elValue > allPages || elValue == "0") {
            elValue = [...elValue];
            elValue.pop();
            elValue = elValue.join("");
            searchInput.value = elValue;
        } else {
            pageBtn.setAttribute('data-i', elValue);
            pageBtn.dispatchEvent(event);
        }
    });
}

tableInit();
} catch (err) { }

const qsettings = document.querySelector('.qsettings');

if (qsettings) {
    qsettings.addEventListener('input', () => {
        if (qsettings.value != "") {
            if (parseInt(qsettings.value) < parseInt(qsettings.min)) {
                qsettings.value = qsettings.min;
            }
            if (parseInt(qsettings.value) > parseInt(qsettings.max)) {
                qsettings.value = qsettings.max;
            }
        }
        else {
            qsettings.value = qsettings.min;
        }
    })
}

const tsettings = document.querySelector('.tsettings');
if (tsettings) {
    tsettings.addEventListener('input', () => {
        if (tsettings.value != "") {
            if (parseInt(tsettings.value) < parseInt(tsettings.min)) {
                tsettings.value = tsettings.min;
            }
            if (parseInt(tsettings.value) > parseInt(tsettings.max)) {
                tsettings.value = tsettings.max;
            }
        }
        else {
            tsettings.value = tsettings.min;
        }
    })
}

const attempts = document.querySelector('.attempts');
if (attempts) {
    attempts.addEventListener('input', () => {
        if (attempts.value != "") {
            if (parseInt(attempts.value) < parseInt(attempts.min)) {
                attempts.value = attempts.min;
            }
            if (parseInt(attempts.value) > parseInt(attempts.max)) {
                attempts.value = attempts.max;
            }
        }
        else {
            attempts.value = 0;
        }
    })
}

const checkAll = document.querySelector('input[name="All"]');
const checkId = document.querySelectorAll('input[name="users"]');

if (checkAll) {
    checkAll.addEventListener('change', () => {
        if (checkAll.checked) {
            for (let i = 0; i < checkId.length; i++) {
                checkId[i].checked = true;
            }
        }
        else {
            for (let i = 0; i < checkId.length; i++) {
                checkId[i].checked = false;
            }
        }
    })
}