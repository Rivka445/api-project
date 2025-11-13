
const extrctDataFromInputUser = () => {
    const userName = document.querySelector("#userName").value
    const firstName = document.querySelector("#firstName").value
    const lastName = document.querySelector("#lastName").value
    const password = document.querySelector("#password").value
    const id=1
    return {id, userName, firstName, lastName, password }
}

const extrctDataFromInputLogIn = () => {
    const userName = document.querySelector("#username").value
    const password = document.querySelector("#pasword").value
    const   id = 1,firstName= "aaa", lastName ="aaa"
    return { id, userName, firstName , lastName , password }
}

async function registIn() {
    const check = await checkPassword()
    if (check === 0)
        alert("הסיסמא חלשה מידי אנא בדוק חוזק סיסמא")
    else {
        const newUser = extrctDataFromInputUser()
        try {
            const response = await fetch(
                "https://localhost:44362/api/Users", {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(newUser)
            }
            )
            if (!response.ok) {
                throw new Error(`HTTP error! status ${response.status}`);
            }
            else {
                alert("המשתמש נרשם בהצלחה")
                const newUserFull = await response.json()
            }
        }
        catch (e) { alert(e) }
    }
}

async function logIn() {
    const existUser = extrctDataFromInputLogIn()
    try {
        const response = await fetch(
            "https://localhost:44362/api/Users/login",{
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(existUser)
            }
        )
        if (!response.ok) {
            alert("שם משתמש או סיסמא שגויים")
            throw new Error(`HTTP error! status ${response.status}`);
        }
        else {
            const fullUser = await response.json()
            sessionStorage.setItem("currentUser", JSON.stringify(fullUser))
            window.location.href = "page2.html"
        }
    }
    catch (e) { alert(e) }
}
async function checkPassword() {
    const password = document.querySelector("#password").value
    const userPassword ={ password }
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
                const p="1"
                const userPassword = {p}
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
                bar.style.display="flex"
                const array = []
                for (let i = 0; i < a; i++) {  
                    const step = document.createElement("div")
                    step.className ="stage"
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


