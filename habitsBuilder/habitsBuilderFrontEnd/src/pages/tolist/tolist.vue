<script>
export default {
  data() {
    return {
      todos: [
        {
          id: 123123,
          title: "内容1",
          completed: false
        },
        {
          id: 5678888,
          title: "内容2",
          completed: true
        }
      ],
      visibility: 'all'
    };
  },
  methods: {
    toggleAll(e) {
      this.todos.forEach(todo => {
        todo.completed = e.target.checked;
      });
    },
    onHashChange() {
      const hash = window.location.hash.replace(/#\/?/, '');
      if (['all', 'active', 'completed'].includes(hash)) {
        this.visibility = hash;
      } else {
        window.location.hash = '';
        this.visibility = 'all';
      }
    },
    addTodo (e) {
      const title = e.target.value.trim();
      if (!title) {
        return;
      }
      this.todos.push({
        id: Date.now(),
        title: title,
        completed: false
      });
      e.target.value = '';
    },
    removeTodo(todo) {
      this.todos.splice(this.todos.indexOf(todo), 1);
    },
    clearCompleted() {
      this.todos = this.todos.filter(todo => !todo.completed);
    }
  },
  computed: {
    filteredTodos() {
      switch (this.visibility) {
        case 'all':
          return this.todos;
        case 'active':
          return this.todos.filter(todo => !todo.completed);
        case 'completed':
          return this.todos.filter(todo => todo.completed);
      }
    },
    remaining() {
      return this.todos.filter(todo => !todo.completed).length;
    }
  },
  mounted() {
    window.addEventListener('hashchange', this.onHashChange);
    this.onHashChange();
  }
}
</script>

<template>
  <section class="todoapp">
    <header class="header">
      <h1>todos</h1>
      <input @keyup.enter="addTodo" class="new-todo" placeholder="请输入待办事项" autofocus>
    </header>

    <section class="main">
      <input id="toggle-all" class="toggle-all" type="checkbox" @click="toggleAll">
      <label for="toggle-all">Mark all as complete</label>
      <ul class="todo-list">
        <li v-for="todo in filteredTodos" :key="todo.id" :class="{ 'completed': todo.completed }">
          <div class="view">
            <input class="toggle" type="checkbox" v-model="todo.completed">
            <label>{{ todo.title }}</label>
            <button class="destroy" @click="removeTodo(todo)"></button>
          </div>
          <input class="edit" value="Create a TodoMVC template">
        </li>
      </ul>
    </section>

    <footer class="footer">
      <span class="todo-count"><strong>{{ remaining }}</strong>项未打卡</span>
      <ul class="filters">
        <li>
          <a :class="{selected: visibility === 'all'}" href="#/all">全部事项</a>
        </li>
        <li>
          <a :class="{selected: visibility === 'active'}" href="#/active">未打卡事项</a>
        </li>
        <li>
          <a :class="{selected: visibility === 'completed'}" href="#/completed">已打卡事项</a>
        </li>
      </ul>
      <button class="clear-completed" @click="clearCompleted">清除已打卡事项</button>
    </footer>
  </section>
</template>

<style>
@import "https://unpkg.com/todomvc-app-css@2.4.1/index.css";
</style>
