using Microsoft.AspNetCore.Components;

using System.Net.Http.Json;

using TaskManagerClient.Model;

namespace TaskManagerClient.Pages
{
    public partial class Home
    {
        [Inject] public HttpClient Http { get; set; }

        private IList<TaskItem>? tasks;
        private string? error;
        private string? newTask;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                string requestUri = "api/TaskItems";

                tasks = await Http.GetFromJsonAsync<IList<TaskItem>>(requestUri);
            }
            catch (Exception ex)
            {
                error = "Error Encountered";
            };
        }

        protected async Task AddTask()
        {
            if (string.IsNullOrWhiteSpace(newTask))
                return;

            TaskItem newTaskItem = new()
            {
                TaskName = newTask,
                IsComplete = false
            };

            tasks!.Add(newTaskItem);
            string requestUri = "api/TaskItems";

            var response = await Http.PostAsJsonAsync(requestUri, newTaskItem);

            if (response.IsSuccessStatusCode)
            {
                newTask = string.Empty;
            }
            else
            {
                error = response.ReasonPhrase;
            };

        }

        private async Task DeleteTask(TaskItem taskItem)
        {
            tasks!.Remove(taskItem);

            string requestUri = $"api/TaskItems/{taskItem.TaskItemId}";

            var response = await Http.DeleteAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                error = response.ReasonPhrase;
            };
        }

        private async Task CheckboxChecked(TaskItem task)
        {
            task.IsComplete = !task.IsComplete;

            string requestUri = $"api/TaskItems/{task.TaskItemId}";

            var response = await Http.PutAsJsonAsync<TaskItem>(requestUri, task);

            if (!response.IsSuccessStatusCode)
            {
                error = response.ReasonPhrase;
            };
        }
    }
}
