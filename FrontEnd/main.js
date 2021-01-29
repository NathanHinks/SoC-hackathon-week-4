const mainList = document.querySelector('#main-list');
const inputForm = document.querySelector('#input-form');

const BACKEND_URL = 'http://localhost:5000';

async function loadInitialToDos() {
	const res = await fetch(`${BACKEND_URL}/todoitems`);
	const data = await res.json();
	data.forEach(renderToDo);
}

function renderToDo(toDoItem) {
	const { id, title, priority, isComplete } = toDoItem;

	const span = document.createElement('span');
	span.innerHTML = `${title} - <span class="priority"> priority: ${priority}</span>`;

	const li = document.createElement('li');
	li.id = `to-do-item-${id}`;
	const checkBox = (createCheckbox(toDoItem));
	if (isComplete) {
		li.classList.add('completed');
		checkBox.checked = true;
	}
	li.appendChild(checkBox);
	li.appendChild(span);
	li.appendChild(createDeleteButton(toDoItem));
	mainList.appendChild(li);
}

function createCheckbox(toDoItem) {
	const checkBox = document.createElement('input');
	checkBox.type = 'checkbox';
	checkBox.addEventListener('change', () => {
		toggleToDoComplete(toDoItem);
	});
	return checkBox;
}

function createDeleteButton(toDoItem) {
	const button = document.createElement('button');
	button.innerText = 'DELETE';
	button.addEventListener('click', () => {
		deleteToDo(toDoItem);
	});
	return button;
}

async function handleAddToDo(event) {
	// stops page reloading by default
	event.preventDefault();
	// get title and priority from form
	const partialTodo = {
		title    : event.target[0].value,
		priority : event.target[1].value,
	};
	// sent to database and recieve full item including id
	const completeTodo = await sendToDo('/todoitems', 'POST', partialTodo);
	event.target.reset();
	renderToDo(completeTodo);
}

async function sendToDo(path, method, body = '') {
	const res = await fetch(`${BACKEND_URL}${path}`, {
		method,
		headers : {
			'content-type' : 'application/json',
		},
		body    : JSON.stringify(body),
	});
	const data = await res.json();
	return data;
}

async function toggleToDoComplete(toDoItem) {
	console.log(toDoItem);
	const li = document.querySelector(`#to-do-item-${toDoItem.id}`);
	await sendToDo(`/todoitems/${toDoItem.id}`, 'PUT', {
		...toDoItem,
		isComplete : !li.classList.contains('completed'),
	});
	li.classList.toggle('completed');
}

async function deleteToDo(toDoItem) {
	const res = await fetch(`${BACKEND_URL}/todoitems/${toDoItem.id}`, {
		method : 'DELETE',
	});
	if (res.ok) {
		document.querySelector(`#to-do-item-${toDoItem.id}`).remove();
	}
}

inputForm.addEventListener('submit', handleAddToDo);

loadInitialToDos();

//delete all button
const deleteCompletedBtn = document.querySelector('.delete-completed');

async function deleteAllToDo() {
	const res = await fetch(`${BACKEND_URL}/todoitems/`, {
		method : 'DELETE',
	});
	if (res.ok) {
		location.reload();
	}
}

deleteCompletedBtn.addEventListener('click', deleteAllToDo);


//filter by priority button
const priorityBtn = document.querySelector('.filter-priority');

  //orders on load anyway
priorityBtn.addEventListener('click', () => location.reload());
