# ExpenseMate - Implementation Plan

**Version:** 1.0
**Date:** December 2024
**Document Type:** Technical Implementation Strategy

---

## 1. EXECUTIVE SUMMARY

### 1.1 What We're Building
ExpenseMate is a personal expense tracking mobile app built with .NET MAUI. It allows users to track transactions, manage budgets, and view financial insights - all stored locally on the device.

### 1.2 How We're Building It
We are extending an existing VanillaSlice architecture codebase. This means we:
- Follow the established vertical slice pattern
- Use the existing base classes and interfaces
- Extend rather than rewrite
- Maintain consistency with current naming conventions

### 1.3 Development Approach
- **Methodology**: Incremental feature delivery in 3 phases
- **Architecture**: Vertical Slice (feature-centric)
- **Pattern**: MVVM with Services
- **Data**: Local SQLite with EF Core
- **UI**: Native XAML (not Blazor)

---

## 2. ARCHITECTURAL APPROACH

### 2.1 VanillaSlice Pattern Overview

The project follows **Vertical Slice Architecture** where code is organized by feature, not by technical layer.

**Traditional Layered (NOT what we're using):**
```
├── Controllers/
├── Services/
├── Repositories/
├── Models/
```

**Vertical Slice (WHAT we're using):**
```
├── Features/
│   ├── Transactions/
│   │   ├── All transaction-related code
│   ├── Categories/
│   │   ├── All category-related code
│   ├── Dashboard/
│   │   ├── All dashboard-related code
```

### 2.2 Why This Approach?

| Benefit | Explanation |
|---------|-------------|
| **Cohesion** | All code for a feature lives together - easier to understand |
| **Independence** | Features can be developed/modified without affecting others |
| **Testability** | Each slice can be tested in isolation |
| **Scalability** | New features = new folders, no touching existing code |
| **Consistency** | Already established in the repo, we just extend it |

### 2.3 Layer Responsibilities

```
┌─────────────────────────────────────────────────────────────┐
│                      MAUI App Layer                         │
│  ┌─────────────────────────────────────────────────────┐   │
│  │                    Features/                         │   │
│  │  ┌────────────┐  ┌────────────┐  ┌────────────┐     │   │
│  │  │ Listing/   │  │ Form/      │  │ ViewModels/│     │   │
│  │  │  - Page    │  │  - Page    │  │  - DTOs    │     │   │
│  │  │  - VM      │  │  - VM      │  │            │     │   │
│  │  └────────────┘  └────────────┘  └────────────┘     │   │
│  └─────────────────────────────────────────────────────┘   │
│                           │                                 │
│                           ▼                                 │
│  ┌─────────────────────────────────────────────────────┐   │
│  │              Service Contracts Layer                 │   │
│  │         (Interfaces + Business Models)               │   │
│  └─────────────────────────────────────────────────────┘   │
│                           │                                 │
│                           ▼                                 │
│  ┌─────────────────────────────────────────────────────┐   │
│  │              Server DataServices Layer               │   │
│  │      (Implementations + Database Access)             │   │
│  └─────────────────────────────────────────────────────┘   │
│                           │                                 │
│                           ▼                                 │
│  ┌─────────────────────────────────────────────────────┐   │
│  │              Server Data Layer                       │   │
│  │         (EF Core + SQLite + Entities)                │   │
│  └─────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
```

### 2.4 Data Flow Pattern

```
User Action (UI)
       │
       ▼
   ViewModel (Commands/Properties)
       │
       ▼
   Service Interface (Contract)
       │
       ▼
   Service Implementation (Business Logic)
       │
       ▼
   EF Core DbContext
       │
       ▼
   SQLite Database
```

---

## 3. PROJECT STRUCTURE STRATEGY

### 3.1 Solution Organization

We maintain the existing structure and add new features:

```
src/
├── ExpenseTracker.Base/
│   └── ExpenseTracker.Framework/          # [EXISTING] Core interfaces, base classes
│                                          # We ADD: new interfaces if needed
│
├── ExpenseTracker.Common/
│   ├── ExpenseTracker.Common/             # [EXTEND] Add new enums
│   │   └── Enums/
│   │       ├── TransactionType.cs         # NEW
│   │       ├── AccountType.cs             # NEW
│   │       └── RecurrenceType.cs          # NEW
│   │
│   └── ExpenseTracker.Server.Data/        # [EXTEND] Add new entities
│       └── EF/
│           ├── Category.cs                # NEW
│           ├── Transaction.cs             # NEW
│           ├── Account.cs                 # NEW
│           ├── Budget.cs                  # NEW
│           └── AppSettings.cs             # NEW
│
├── ExpenseTracker.Platform/
│   ├── ExpenseTracker.ServiceContracts/   # [EXTEND] Add new contracts
│   │   └── Features/
│   │       ├── Categories/                # NEW folder
│   │       ├── Transactions/              # NEW folder
│   │       ├── Dashboard/                 # NEW folder
│   │       └── Budgets/                   # NEW folder
│   │
│   └── ExpenseTracker.Server.DataServices/# [EXTEND] Add implementations
│       └── Features/
│           ├── Categories/                # NEW folder
│           ├── Transactions/              # NEW folder
│           ├── Dashboard/                 # NEW folder
│           └── Budgets/                   # NEW folder
│
└── ExpenseTracker.MauiNativeApp/          # [EXTEND] Add UI features
    ├── Features/
    │   ├── Categories/                    # NEW folder
    │   ├── Transactions/                  # NEW folder
    │   ├── Dashboard/                     # NEW folder
    │   └── Budgets/                       # NEW folder
    ├── Resources/
    │   └── Styles/
    │       ├── Colors.xaml                # MODIFY for new palette
    │       └── Styles.xaml                # MODIFY for new components
    └── Views/
        └── AppShellTabs.xaml              # MODIFY for new navigation
```

### 3.2 Feature Folder Structure

Each feature follows this consistent structure:

```
Features/{FeatureName}/
├── Listing/                    # List view of items
│   ├── {Name}ListPage.xaml     # XAML layout
│   ├── {Name}ListPage.xaml.cs  # Code-behind (minimal)
│   └── {Name}ListPageViewModel.cs  # ViewModel with commands/properties
│
├── Form/                       # Add/Edit form
│   ├── {Name}FormPage.xaml
│   ├── {Name}FormPage.xaml.cs
│   └── {Name}FormPageViewModel.cs
│
└── ViewModels/                 # Data transfer objects
    ├── {Name}ListingViewModel.cs   # Display model for list items
    ├── {Name}FormViewModel.cs      # Editable model for forms
    └── {Name}FilterViewModel.cs    # Filter criteria model
```

---

## 4. DEVELOPMENT PHASES

### 4.1 Phase Overview

```
┌─────────────────────────────────────────────────────────────┐
│  PHASE 0: Foundation                                        │
│  ├─ Theme System                                            │
│  ├─ Database Setup                                          │
│  └─ Navigation Shell                                        │
├─────────────────────────────────────────────────────────────┤
│  PHASE 1: MVP (Minimum Viable Product)                      │
│  ├─ Categories CRUD                                         │
│  ├─ Transactions CRUD + List                                │
│  └─ Dashboard Summary                                       │
├─────────────────────────────────────────────────────────────┤
│  PHASE 2: Core Finance                                      │
│  ├─ Budgets                                                 │
│  ├─ Accounts/Wallets                                        │
│  └─ Recurring Transactions                                  │
├─────────────────────────────────────────────────────────────┤
│  PHASE 3: Pro Features                                      │
│  ├─ Reports & Charts                                        │
│  ├─ Backup/Restore                                          │
│  └─ Security (PIN/Biometric)                                │
└─────────────────────────────────────────────────────────────┘
```

### 4.2 Phase 0: Foundation (Do First)

**Purpose**: Set up the infrastructure that all features will use.

#### Task 0.1: Theme System
**What**: Create a consistent design system with colors, typography, and component styles.
**Why**: Every screen needs consistent styling. Doing this first prevents rework.
**How**:
1. Update `Colors.xaml` with new color palette
2. Update `Styles.xaml` with component styles
3. Create semantic color names (Primary, Surface, etc.)
4. Support both light and dark themes

#### Task 0.2: Database Setup
**What**: Create all database entities and configure EF Core.
**Why**: Features need data persistence from day one.
**How**:
1. Create entity classes in Server.Data
2. Add DbSets to AppDbContext
3. Create database seeder for default data
4. Ensure EnsureCreated runs on app startup

#### Task 0.3: Navigation Shell
**What**: Set up the tab bar navigation structure.
**Why**: Users need to navigate between features.
**How**:
1. Update AppShellTabs.xaml with new tabs
2. Define routes for all screens
3. Create placeholder pages for each tab

### 4.3 Phase 1: MVP

**Goal**: User can track transactions with categories and see a summary.

#### Task 1.1: Categories Feature
**Sequence**: Build first because Transactions depend on it.

**Steps**:
1. Create Service Contracts (interfaces + business models)
2. Create Server DataServices (implementations)
3. Create MAUI Feature (pages + viewmodels)
4. Register in DI container
5. Seed default categories

**Deliverables**:
- Category list page with grid/list view
- Category form page for add/edit
- Delete with reassignment dialog
- 11 default seeded categories

#### Task 1.2: Transactions Feature
**Sequence**: Build after Categories (has dependency).

**Steps**:
1. Create Service Contracts
2. Create Server DataServices
3. Create MAUI Feature
4. Register in DI container
5. Add FAB for quick add

**Deliverables**:
- Transaction list grouped by date
- Transaction form with all fields
- Search functionality
- Filter panel (date, category, type)
- Daily totals in group headers

#### Task 1.3: Dashboard Feature
**Sequence**: Build last in Phase 1 (reads from Transactions).

**Steps**:
1. Create Dashboard service for aggregations
2. Create Dashboard page and viewmodel
3. Implement month selector
4. Implement summary cards
5. Implement category breakdown
6. Implement recent transactions section

**Deliverables**:
- Month navigation
- Summary cards (expense, income, net)
- Category breakdown list
- Recent transactions preview

### 4.4 Phase 2: Core Finance

#### Task 2.1: Budgets Feature
**What**: Allow users to set spending limits.
**Dependencies**: Categories, Transactions

**Steps**:
1. Create Budget entity and contracts
2. Create budget list showing progress
3. Create budget form for setting amounts
4. Calculate spent vs budget from transactions
5. Add visual progress indicators

#### Task 2.2: Accounts Feature
**What**: Track multiple wallets/accounts.
**Dependencies**: None (but integrates with Transactions)

**Steps**:
1. Create Account entity and contracts
2. Create account list and form
3. Add account selector to transaction form
4. Calculate running balance per account

#### Task 2.3: Recurring Transactions
**What**: Auto-generate repeating transactions.
**Dependencies**: Transactions, Categories

**Steps**:
1. Create RecurringTransaction entity
2. Create UI for managing recurring items
3. Implement processor to generate transactions
4. Run processor on app startup

### 4.5 Phase 3: Pro Features

#### Task 3.1: Reports
**What**: Visualize trends over time.
**Approach**: Simple progress bars initially, charts later if time permits.

#### Task 3.2: Backup/Restore
**What**: Export and import data.
**Approach**: JSON format, save to device storage.

#### Task 3.3: Security
**What**: PIN and biometric lock.
**Approach**: Use MAUI SecureStorage and device biometric APIs.

---

## 5. TECHNICAL DECISIONS

### 5.1 Why SQLite?
| Consideration | Decision |
|---------------|----------|
| Offline-first requirement | SQLite works without network |
| Data size | Expense data is small, SQLite handles it easily |
| Complexity | No server setup needed |
| Performance | Fast for local operations |
| Existing setup | Project already configured for SQLite |

### 5.2 Why MVVM Pattern?
| Consideration | Decision |
|---------------|----------|
| Testability | ViewModels can be unit tested |
| Separation | UI logic separate from business logic |
| Data binding | MAUI has excellent binding support |
| Existing pattern | Project already uses this pattern |

### 5.3 Why Not Blazor?
| Consideration | Decision |
|---------------|----------|
| User requirement | Explicitly requested native XAML |
| Performance | Native is faster than webview |
| Consistency | Project is already native MAUI |

### 5.4 State Management Approach
- **Local state**: ViewModel properties with INotifyPropertyChanged
- **Shared state**: Services injected via DI
- **Persistent state**: SQLite database
- **User preferences**: SecureStorage for settings

### 5.5 Navigation Strategy
- **Shell-based**: Using MAUI Shell for tabbed navigation
- **URI routing**: Navigate using routes like `//transactions/form?id=123`
- **Parameters**: Pass IDs via query parameters, load data in ViewModel

---

## 6. SERVICE LAYER DESIGN

### 6.1 Service Types

| Service Type | Purpose | Example |
|--------------|---------|---------|
| **Listing** | Get paginated/filtered lists | GetTransactionsAsync(filter) |
| **Form** | CRUD operations | GetById, Create, Update, Delete |
| **SelectList** | Get dropdown options | GetCategoriesForPicker() |
| **Analytics** | Aggregations and calculations | GetMonthlySummary() |

### 6.2 Service Interface Pattern

Each feature has service interfaces in ServiceContracts:

```
ITransactionListingDataService
├── GetPaginatedItemsAsync(filter) → PagedList<TransactionListingBusinessModel>

ITransactionFormDataService
├── GetByIdAsync(id) → TransactionFormBusinessModel
├── CreateAsync(model) → string (new ID)
├── UpdateAsync(id, model) → string
├── DeleteAsync(id) → int (rows affected)

ICategorySelectListDataService
├── GetSelectListAsync() → List<CategorySelectListBusinessModel>
```

### 6.3 Business Model Pattern

Three types of models per feature:

| Model Type | Purpose | Location |
|------------|---------|----------|
| **ListingBusinessModel** | Display data in lists | ServiceContracts |
| **FormBusinessModel** | Data for forms (CRUD) | ServiceContracts |
| **FilterBusinessModel** | Filter/search criteria | ServiceContracts |

Plus view-specific models in MAUI:

| Model Type | Purpose | Location |
|------------|---------|----------|
| **ListingViewModel** | Bind to list item UI | MauiNativeApp |
| **FormViewModel** | Bind to form UI | MauiNativeApp |
| **FilterViewModel** | Bind to filter UI | MauiNativeApp |

---

## 7. UI/UX APPROACH

### 7.1 Design Philosophy
- **Mobile-first**: Design for small screens, scale up for tablets/desktop
- **Touch-friendly**: Large tap targets (44dp minimum)
- **Consistent**: Same patterns across all screens
- **Accessible**: Support screen readers, respect font scaling
- **Feedback-rich**: Loading states, success/error messages, empty states

### 7.2 Component Strategy

| Component | Approach |
|-----------|----------|
| **List items** | Reusable DataTemplate with consistent layout |
| **Forms** | Vertical stack of labeled inputs |
| **Cards** | Rounded corners, subtle shadow, surface color |
| **Buttons** | Primary (filled), Secondary (outline), Danger (red) |
| **Inputs** | Consistent height, clear labels, validation feedback |

### 7.3 Empty States Strategy

Every list has an empty state with:
1. Friendly illustration or icon
2. Helpful message explaining what goes here
3. Call-to-action button to add first item

Example:
```
┌─────────────────────────────────────┐
│                                     │
│           📝                        │
│                                     │
│    No transactions yet              │
│                                     │
│    Tap the + button to add your     │
│    first expense or income          │
│                                     │
│    [ Add Transaction ]              │
│                                     │
└─────────────────────────────────────┘
```

### 7.4 Loading States Strategy

- Show ActivityIndicator during data fetch
- Use skeleton screens for better perceived performance (optional)
- Disable buttons during save operations
- Show progress for long operations

---

## 8. DATA SEEDING APPROACH

### 8.1 When Seeding Happens
- On first app launch (database is empty)
- After data reset in settings
- NOT after restore (restore includes categories)

### 8.2 What Gets Seeded
1. **Default Categories** (11 categories with icons and colors)
2. **Default Account** (Cash wallet)
3. **Default Settings** (USD, Monday start, System theme)

### 8.3 Seeding Logic
```
On app startup:
  If Categories table is empty:
    Insert default categories
    Insert default account
    Initialize settings
```

---

## 9. VALIDATION STRATEGY

### 9.1 Where Validation Happens

| Layer | Type | Examples |
|-------|------|----------|
| **ViewModel** | UI validation | Required fields, format |
| **Service** | Business validation | Uniqueness, relationships |
| **Database** | Constraints | NOT NULL, foreign keys |

### 9.2 Validation Rules

**Transaction:**
- Amount: Required, must be > 0
- Category: Required, must exist
- Date: Required
- Type: Required (Expense/Income)

**Category:**
- Name: Required, 1-50 characters, unique
- Icon: Required
- Color: Required

**Budget:**
- Amount: Required, must be > 0
- Month: Required, 1-12
- Year: Required

### 9.3 Validation Feedback
- Real-time feedback as user types (where appropriate)
- Highlight invalid fields with red border
- Show error message below field
- Disable save button until valid

---

## 10. ERROR HANDLING STRATEGY

### 10.1 Error Categories

| Category | Handling |
|----------|----------|
| **Validation errors** | Show inline field errors |
| **Database errors** | Show user-friendly message, log details |
| **Unexpected errors** | Show generic message, offer retry |

### 10.2 Error Messages
- User-friendly language (no technical jargon)
- Actionable when possible ("Try again", "Check your input")
- Consistent styling (danger color, icon)

---

## 11. TESTING APPROACH

### 11.1 Test Types

| Type | What | Coverage |
|------|------|----------|
| **Unit tests** | Services, calculations | Business logic |
| **Integration tests** | Database operations | Data layer |
| **UI tests** | (Optional) Navigation, forms | User flows |

### 11.2 Priority Test Areas
1. Dashboard calculations (sums, percentages)
2. Budget progress calculations
3. Transaction filtering logic
4. Date grouping logic

---

## 12. IMPLEMENTATION CHECKLIST

### Phase 0: Foundation
- [ ] Update Colors.xaml with new palette
- [ ] Update Styles.xaml with component styles
- [ ] Create database entities
- [ ] Update AppDbContext
- [ ] Create database seeder
- [ ] Update AppShellTabs navigation
- [ ] Verify app runs with new structure

### Phase 1: MVP
- [ ] Categories: Service contracts
- [ ] Categories: Server data services
- [ ] Categories: MAUI pages and viewmodels
- [ ] Categories: Test CRUD operations
- [ ] Transactions: Service contracts
- [ ] Transactions: Server data services
- [ ] Transactions: MAUI pages and viewmodels
- [ ] Transactions: Test CRUD + filters
- [ ] Dashboard: Service contracts
- [ ] Dashboard: Server data service
- [ ] Dashboard: MAUI page and viewmodel
- [ ] Dashboard: Test summary calculations
- [ ] End-to-end test of full flow

### Phase 2: Core Finance
- [ ] Budgets feature complete
- [ ] Accounts feature complete
- [ ] Recurring transactions feature complete
- [ ] Budget alerts working

### Phase 3: Pro Features
- [ ] Reports feature complete
- [ ] Backup/restore working
- [ ] App lock (PIN) working
- [ ] All settings functional

---

## 13. DEPENDENCIES & PACKAGES

### 13.1 Existing Packages (Keep)
- Microsoft.EntityFrameworkCore.Sqlite
- CommunityToolkit.Maui
- CommunityToolkit.Mvvm
- Newtonsoft.Json

### 13.2 New Packages (If Needed)
| Package | Purpose | When |
|---------|---------|------|
| LiveChartsCore.SkiaSharpView.Maui | Charts | Phase 3 (Reports) |
| Plugin.Fingerprint | Biometric auth | Phase 3 (Security) |

### 13.3 Package Policy
- Prefer existing packages
- Add new only when necessary
- Document reason for each new package

---

## 14. RISK MITIGATION

### 14.1 Identified Risks

| Risk | Impact | Mitigation |
|------|--------|------------|
| Architecture deviation | High | Follow existing patterns strictly |
| Performance issues | Medium | Use virtualization, test with large data |
| Theme inconsistency | Medium | Build theme system first |
| Data loss | High | Implement backup early |

### 14.2 Quality Gates
Before each phase completion:
1. All features tested manually
2. App runs on Android emulator
3. No build errors or warnings (if possible)
4. Theme consistent across all screens

---

## 15. SUCCESS CRITERIA

### Phase 1 Success
- [ ] User can add a transaction in under 30 seconds
- [ ] User can find a past transaction using search
- [ ] Dashboard shows accurate monthly totals
- [ ] App works offline
- [ ] Light and dark themes work

### Phase 2 Success
- [ ] User can set and track budgets
- [ ] User can manage multiple accounts
- [ ] Recurring transactions generate automatically

### Phase 3 Success
- [ ] User can view spending trends
- [ ] User can backup and restore data
- [ ] User can lock app with PIN

---

## 16. APPROVAL & SIGN-OFF

### Questions for Approval

1. **Phase Priority**: Is Phase 1 → 2 → 3 order acceptable?
2. **Feature Scope**: Any features to add or remove?
3. **Color Palette**: Is blue primary + green/red semantic acceptable?
4. **Default Categories**: Are the 11 categories appropriate?
5. **Navigation**: Is bottom tab bar the right approach?

### Ready to Proceed?
After approval of this plan and the Requirements document, implementation will begin with Phase 0 (Foundation).

---

**END OF IMPLEMENTATION PLAN**
