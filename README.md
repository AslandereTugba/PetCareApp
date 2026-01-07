# PetCareApp (.NET MAUI)

PetCareApp is a simple cross-platform pet care tracker built with **.NET MAUI** and **SQLite**.  
It helps users manage pets, create recurring care tasks (with a frequency in days), mark tasks as done, and keep a history of completed actions (care logs). Vet visits can also be recorded with a checklist of completed tasks.

## What the app does

### Main features
- **Pets CRUD**: Add / Edit / Delete pets
- **Tasks per pet**: Add / Edit / Delete care tasks (e.g., walk, vaccine, feeding)
- **Task completion logic (Done button)**:
  - Updates task `LastDone` and calculates the next due date (`NextDue`)
  - Creates a **CareLog** entry (history)
- **Dashboard (Home)**:
  - Shows **Overdue Tasks**
  - Shows **Today’s Tasks**
  - Shows **Today’s Completed Tasks** (based on logs created today)
- **Care Log page**:
  - Displays the completion history per pet
  - Uses a snapshot field so old log entries can keep the old task name if the task name changes
- **Vet Visit page**:
  - Add a visit date + notes
  - Optional checklist to mark multiple tasks completed during that visit

## Entities (Database Tables)
- **Pet**
- **CareTask** (belongs to a Pet)
- **CareLog** (created when a task is completed; stores completion history)
- **VetVisit** (belongs to a Pet)

## Pages & Navigation
- **Home (MainPage)**: Task dashboard (Overdue / Today / Completed Today)
- **PetListPage**: List pets, add new pet, select a pet
- **AddPetPage**: Add or edit a pet
- **PetDetailPage**: See pet details + tasks, mark task done, go to Vet / Logs
- **AddTaskPage**: Add or edit a task for the selected pet
- **VetVisitPage**: Add vet visits and optionally complete tasks via checklist
- **CareLogPage**: See care history (task completions) for the selected pet

Navigation uses **Shell routing** and query parameters (e.g., `?petId=3`).

## Business rules (simple logic)
- When a task is completed:
  - `LastDone = now`
  - `NextDue = LastDone + FrequencyDays`
  - A `CareLog` record is inserted (with `TaskNameSnapshot`)

- Dashboard:
  - **Overdue Tasks**: tasks where `NextDue < today` (excluding tasks already completed today)
  - **Today’s Tasks**: tasks where `NextDue == today` (excluding tasks already completed today)
  - **Today’s Completed Tasks**: logs created today

## Tech stack
- **.NET MAUI**
- **C#**
- **SQLite** (local database)
- Repository pattern (simple data access classes)

## Folder notes
- Models are located under the `Models/` folder (moved for cleaner structure).
- Pages are under `Views/`
- Task logic is in `Services/TaskService.cs`
- Data access is in `Repositories/`

## How to run
1. Open the solution in **Visual Studio 2022** (with MAUI workload installed).
2. Restore NuGet packages.
3. Select a target:
   - **Windows Machine** for quick testing, or Android emulator/device.
4. Run.

## Suggested usage flow
1. Add a pet from **Pets → Add**
2. Tap a pet to open **Pet Details**
3. Add tasks (**Add Task**) and set their frequency (days) + due date
4. Use **Done** on tasks to generate logs and update the schedule
5. Check **Home** dashboard for today/overdue/completed tasks
6. Open **Logs** to see history or **Vet** to record vet visits

## Future improvements (optional)
- Pet icons (dog/cat) in the list
- Simple search/filter for pets/tasks
- Better UI layout (Grid + CollectionView)
- Basic statistics (e.g., tasks completed this week)

---
Author: *Tuğba Aslandere*
