window.addEventListener("load", solve);
function solve() {
    const addButton=document.getElementById("add-btn");
    const deleteButton=document.querySelector(".btn.delete");
    const expenseInput=document.getElementById("expense");
    const amountInput=document.getElementById("amount");
    const dateInput=document.getElementById("date");
    const preview=document.getElementById("preview-list")
    const expenses=document.getElementById("expenses-list")
    addButton.addEventListener('click',()=>{
        const expense=expenseInput.value;
    }

}