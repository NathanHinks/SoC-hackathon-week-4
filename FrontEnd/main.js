const mainList = document.querySelector("#main-list");
const inputForm = document.querySelector("#input-form");
const itemsArray = []

const BACKEND_URL = "http://localhost:5000";

async function loadInitialToDos() {
  const res = await fetch(`${BACKEND_URL}/todoitems`);
  const data = await res.json();
  data.forEach(renderToDo);
}

function renderToDo(toDoItem) {
  const { id, title, priority, isComplete } = toDoItem;

  const span = document.createElement("span");
  span.innerText = `${title} - priority: ${priority}`;

  const li = document.createElement("li");
  li.id = `to-do-item-${id}`;
  li.appendChild(createCheckbox(toDoItem));
  li.appendChild(span);
  li.appendChild(createDeleteButton(toDoItem));
  if (isComplete) {
    li.classList.add("completed");
  }
  mainList.appendChild(li);
  itemsArray.push(li)
}

function createCheckbox(toDoItem) {
  const checkBox = document.createElement("input");
  checkBox.type = "checkbox";
  checkBox.addEventListener("change", () => {
    toggleToDoComplete(toDoItem);
  });
  return checkBox;
}

function createDeleteButton(toDoItem) {
  const button = document.createElement("button");
  button.innerText = "DELETE";
  button.addEventListener("click", () => {
    deleteToDo(toDoItem);
  });
  return button;
}

async function handleAddToDo(event) {
  // stops page reloading by default
  event.preventDefault();
  // get title and priority from form
  const partialTodo = {
    title: event.target[0].value,
    priority: event.target[1].value,
  };
  // sent to database and recieve full item including id
  const completeTodo = await sendToDo("/todoitems", "POST", partialTodo);
  event.target.reset();
  renderToDo(completeTodo);
}

async function sendToDo(path, method, body = "") {
  const res = await fetch(`${BACKEND_URL}${path}`, {
    method,
    headers: {
      "content-type": "application/json",
    },
    body: JSON.stringify(body),
  });
  const data = await res.json();
  return data;
}

async function toggleToDoComplete(toDoItem) {
  console.log(toDoItem)
  const li = document.querySelector(`#to-do-item-${toDoItem.id}`);
  await sendToDo(`/todoitems/${toDoItem.id}`, "PUT", {
    ...toDoItem,
    isComplete: !li.classList.contains("completed"),
  });
  li.classList.toggle("completed");
}

async function deleteToDo(toDoItem) {
  const res = await fetch(`${BACKEND_URL}/todoitems/${toDoItem.id}`, {
    method: "DELETE",
  });
  if (res.ok) {
    document.querySelector(`#to-do-item-${toDoItem.id}`).remove();
  }
}

inputForm.addEventListener("submit", handleAddToDo);

loadInitialToDos();


const deleteCompletedBtn = document.querySelector(".delete-completed");
//delete all 
async function deleteAllCompleted() {
  console.log(itemsArray[0]);
  
  
  //loop over all children of main list
  for (i = 0; i < itemsArray.length; i++) {
    if (itemsArray[i].classList.contains("completed")) {
      deleteToDo(itemsArray[i]);
    }
    //check if completed = true
    // if (itemsArray[i].isComplete === true) {
    //if it is delete it
    //deleteToDo(itemsArray[i])
    //itemsArray[i]
    //remove from array
    // }
  }
}
  //itemsArray.length = 0;
  //console.log(itemsArray)



deleteCompletedBtn.addEventListener("click", deleteAllCompleted);