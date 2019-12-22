import Aurelia, { CustomElement } from 'aurelia';
import { TodoApp } from '../src/todo-app';

function createAu(template, ...deps) {
  const wrapper = CustomElement.define({name: 'wrapper', template});
  document.body.appendChild(document.createElement('wrapper'));
  return Aurelia.register(deps).app(wrapper);
}

function cleanup() {
  const wrapper = document.querySelector('wrapper');
  if (wrapper) {
    wrapper.remove();
  }
}

describe('todo-app', () => {
  afterEach(() => {
    cleanup();
  });

  it('should render message', async () => {
    const au = createAu('<todo-app></todo-app>', TodoApp);
    await au.start().wait();
    const node = document.querySelector('todo-app');
    const text =  node.textContent;
    expect(text.trim()).toBe('Hello World!');
    await au.stop().wait();
    cleanup();
  });
});
