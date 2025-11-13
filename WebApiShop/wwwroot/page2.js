const titleName = document.querySelector(".titleName")
const firstName = (JSON.parse(sessionStorage.getItem("currentUser"))).firstName
titleName.textContent = `ברוכה הבאה ${firstName} מייד נצלול פנימה`

const extrctDataFromInput = () => {
    const id = Number(JSON.parse(sessionStorage.getItem("currentUser")).id)
    const userName = document.querySelector("#userName").value
    const firstName = document.querySelector("#firstName").value
    const lastName = document.querySelector("#lastName").value
    const password = document.querySelector("#password").value
    return { id,userName, firstName, lastName, password }
}


async function upDate() {
    const check = await checkPassword()
    if (check === 0)
        alert("הסיסמא חלשה מידי אנא בדוק חוזק סיסמא")
    else {
        const currenrtUser = extrctDataFromInput()
        try {
            const response = await fetch(
                `https://localhost:44362/api/Users/${currenrtUser.id}`,
                {
                    method: `PUT`,
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(currenrtUser)
                }
            )
            if (!response.ok) {
                throw new Error(`HTTP error! status ${response.status}`);
            }
            else {
                sessionStorage.setItem("currentUser", JSON.stringify(currenrtUser))
                alert("המשתמש עודכן בהצלחה")
            }
        }
        catch (e) {
            alert(e)
        }
    }
}
async function checkPassword() {
    const password = document.querySelector("#password").value
    const userPassword = { password }
    try {
        const response = await fetch(
            "https://localhost:44362/api/UsersPassword", {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(userPassword)
        }
        )
        if (!response.ok) {
            throw new Error(`HTTP error! status ${response.status}`);
        }
        else {
            const a = await response.json()
            if (Number(a) < 2) {
                const p = "1"
                const userPassword = { p }
                try {
                    const response = await fetch(
                        "https://localhost:44362/api/UsersPassword", {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(userPassword)
                    }
                    )
                    if (!response.ok)
                        throw new Error(`HTTP error! status ${response.status}`);
                }
                catch (e) {
                    alert(e)
                    return 0
                }
            }
            else {
                const bar = document.querySelector(".bar")
                bar.innerHTML = "";
                bar.style.display = "flex"
                const array = []
                for (let i = 0; i < a; i++) {
                    const step = document.createElement("div")
                    step.className = "stage"
                    array.push(step)
                }
                array.forEach(step => bar.appendChild(step));

            }
        }
    }
    catch (e) {
        alert(e)
        return 0
    }
}
