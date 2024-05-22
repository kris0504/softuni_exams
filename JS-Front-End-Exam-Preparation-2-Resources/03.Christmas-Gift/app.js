const baseURL='http://localhost:3030/jsonstore/gifts/'
const loadButtonElement=document.getElementById('load-presents');
const divlistElement=document.getElementById('gift-list')


const loadPresents=async ()=>{
    const response=await fetch(baseURL);
    const data= await response.json();
    divlistElement.innerHTML = '';

    const giftListFragment = document.createDocumentFragment();
    for (const present of Object.values(data)) {
        const bigDivElement=document.createElement('div');
        bigDivElement.classList.add('gift-sock')
        const contentDivElement=document.createElement('div');
        contentDivElement.classList.add('content')
        const giftElement=document.createElement('p');
        giftElement.textContent=present.gift;
        const nameElement=document.createElement('p');
        nameElement.textContent=present.for;
        const priceElement=document.createElement('p');
        priceElement.textContent=present.price;
        const buttonsDivElement=document.createElement('div');
        buttonsDivElement.classList.add('buttons-container');
        const changeButtonElement=document.createElement('button');
        changeButtonElement.classList.add('change-btn');
        changeButtonElement.textContent='Change';
        const deleteButtonElement=document.createElement('button');
        deleteButtonElement.textContent='Delete';
        deleteButtonElement.classList.add('delete-btn');
        giftListFragment.appendChild(bigDivElement);
        bigDivElement.appendChild(contentDivElement);
        bigDivElement.appendChild(buttonsDivElement);
        contentDivElement.appendChild(giftElement);
        contentDivElement.appendChild(nameElement);
        contentDivElement.appendChild(priceElement);
        buttonsDivElement.appendChild(changeButtonElement);
        buttonsDivElement.appendChild(deleteButtonElement);
        deleteButtonElement.addEventListener('click',deleteGift);
    }
    
    divlistElement.appendChild(giftListFragment);
}
function deleteGift(e) {
    if (e.target.tagName !== 'BUTTON' || !e.target.classList.contains('delete-btn'))  {
        return;
    }
    
    // Get gift  element
    const gift = e.target.parentElement.parentElement;

    // Get id
    const giftId = gift.getAttribute('data-id');

    // Delete request
    fetch(`${baseUrl}/${giftId}`, {
        method: 'DELETE',
    })
        .then(res => {
            if (!res.ok) {
                return;
            }
            
            // remove from giftlist
            gift.remove();
        });
}

loadButtonElement.addEventListener('click',loadPresents);
const addPresent= async()=>{

}