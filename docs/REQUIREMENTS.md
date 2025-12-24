# ExpenseMate - Software Requirements Specification (SRS)

**Version:** 1.0
**Date:** December 2024
**Project Type:** Native .NET MAUI Mobile Application
**Architecture:** VanillaSlice Vertical Slice Pattern

---

## 1. INTRODUCTION

### 1.1 Purpose
This document defines the complete requirements for ExpenseMate, a personal finance tracking mobile application. It serves as the single source of truth for all features, behaviors, and constraints.

### 1.2 Scope
ExpenseMate is an offline-first expense tracking app that allows users to:
- Track daily expenses and income
- Categorize transactions
- Set and monitor budgets
- View financial summaries and reports
- Backup and restore data locally

### 1.3 Target Users
- Individual users managing personal finances
- Users who prefer offline/local data storage
- Mobile-first users (Android primary, Windows secondary)

### 1.4 Definitions
| Term | Definition |
|------|------------|
| Transaction | A single expense or income entry |
| Category | A classification for transactions (e.g., Food, Transport) |
| Budget | A spending limit set for a period |
| Account/Wallet | A source of funds (Cash, Bank, Card) |
| Recurring | A transaction that repeats on a schedule |

---

## 2. OVERALL DESCRIPTION

### 2.1 Product Perspective
- **Standalone Application**: No server dependency, fully offline capable
- **Platform**: .NET MAUI targeting Android (primary) and Windows (secondary)
- **Architecture**: Extends existing VanillaSlice vertical slice structure
- **Data Storage**: Local SQLite database

### 2.2 Product Functions (High-Level)
1. Transaction Management (CRUD)
2. Category Management (CRUD with defaults)
3. Dashboard & Analytics
4. Budget Tracking
5. Multiple Accounts/Wallets
6. Recurring Transactions
7. Reports & Trends
8. Data Backup/Restore
9. App Security (PIN/Biometric)
10. User Preferences & Settings

### 2.3 Operating Environment
| Requirement | Specification |
|-------------|---------------|
| Primary Platform | Android 8.0+ (API 26+) |
| Secondary Platform | Windows 10/11 |
| Framework | .NET 9.0 with MAUI |
| Database | SQLite (local) |
| Connectivity | None required (offline-first) |

### 2.4 Design Constraints
- Must follow VanillaSlice architecture patterns
- Must use native XAML UI (NOT Blazor/Hybrid)
- Must work completely offline
- Must not collect or transmit user data
- Must support light and dark themes

---

## 3. FUNCTIONAL REQUIREMENTS

### 3.1 Transactions Module

#### FR-TXN-001: Add Transaction
| Attribute | Description |
|-----------|-------------|
| ID | FR-TXN-001 |
| Priority | Must Have |
| Description | User can add a new expense or income transaction |

**Inputs:**
| Field | Type | Required | Validation | Default |
|-------|------|----------|------------|---------|
| Amount | Decimal | Yes | Must be > 0 | Empty |
| Type | Enum | Yes | Expense or Income | Expense |
| Category | Reference | Yes | Must exist in categories | None |
| Date | Date | Yes | Any valid date | Today |
| Note | Text | No | Max 500 characters | Empty |
| Payment Method | Text | No | Free text or preset | None |
| Tags | Text | No | Comma-separated | Empty |
| Account | Reference | No | Must exist in accounts | Default account |

**Acceptance Criteria:**
- [ ] Amount field accepts decimal values with 2 decimal places
- [ ] Type toggle clearly shows Expense vs Income
- [ ] Category picker shows all active categories with icons/colors
- [ ] Date picker defaults to today, allows past/future dates
- [ ] Save button disabled until required fields valid
- [ ] Success feedback shown after save
- [ ] Transaction appears in list immediately after save

#### FR-TXN-002: Edit Transaction
| Attribute | Description |
|-----------|-------------|
| ID | FR-TXN-002 |
| Priority | Must Have |
| Description | User can modify an existing transaction |

**Acceptance Criteria:**
- [ ] All original fields pre-populated
- [ ] Same validation rules as Add
- [ ] Changes saved immediately on submit
- [ ] Cancel option available without saving
- [ ] Updated transaction reflects in all views

#### FR-TXN-003: Delete Transaction
| Attribute | Description |
|-----------|-------------|
| ID | FR-TXN-003 |
| Priority | Must Have |
| Description | User can delete a transaction |

**Acceptance Criteria:**
- [ ] Confirmation dialog before deletion
- [ ] Soft delete (mark as deleted) OR hard delete
- [ ] Transaction removed from all views immediately
- [ ] Account balance updated after deletion

#### FR-TXN-004: List Transactions
| Attribute | Description |
|-----------|-------------|
| ID | FR-TXN-004 |
| Priority | Must Have |
| Description | User can view list of all transactions |

**Acceptance Criteria:**
- [ ] Transactions grouped by date (Today, Yesterday, This Week, Earlier)
- [ ] Each group header shows date and daily total
- [ ] Each item shows: category icon, note/description, amount (colored by type)
- [ ] Pull-to-refresh functionality
- [ ] Infinite scroll or pagination for large lists
- [ ] Empty state with call-to-action when no transactions

#### FR-TXN-005: Search Transactions
| Attribute | Description |
|-----------|-------------|
| ID | FR-TXN-005 |
| Priority | Must Have |
| Description | User can search transactions by text |

**Acceptance Criteria:**
- [ ] Search by note/description text
- [ ] Real-time filtering as user types
- [ ] Clear search button
- [ ] Results count displayed
- [ ] Empty search result state

#### FR-TXN-006: Filter Transactions
| Attribute | Description |
|-----------|-------------|
| ID | FR-TXN-006 |
| Priority | Must Have |
| Description | User can filter transactions by multiple criteria |

**Filter Options:**
| Filter | Type | Behavior |
|--------|------|----------|
| Date Range | From-To dates | Include transactions within range |
| Category | Multi-select | Include selected categories only |
| Type | Single-select | All / Expense / Income |
| Amount Range | Min-Max | Include transactions within range |
| Account | Multi-select | Include selected accounts only |

**Acceptance Criteria:**
- [ ] Filter panel accessible via button/icon
- [ ] Multiple filters can be combined (AND logic)
- [ ] Active filter count shown on filter button
- [ ] Clear all filters option
- [ ] Filters persist during session

---

### 3.2 Categories Module

#### FR-CAT-001: Default Categories
| Attribute | Description |
|-----------|-------------|
| ID | FR-CAT-001 |
| Priority | Must Have |
| Description | App provides default categories on first run |

**Default Categories:**
| Name | Icon | Color | Type |
|------|------|-------|------|
| Food & Dining | restaurant | #EF4444 | Expense |
| Transport | car | #3B82F6 | Expense |
| Bills & Utilities | receipt | #8B5CF6 | Expense |
| Shopping | shopping_bag | #EC4899 | Expense |
| Health | medical | #10B981 | Expense |
| Entertainment | movie | #F59E0B | Expense |
| Education | school | #06B6D4 | Expense |
| Gifts & Donations | gift | #F97316 | Expense |
| Salary | payments | #22C55E | Income |
| Other Income | money | #84CC16 | Income |
| Other | more | #6B7280 | Both |

**Acceptance Criteria:**
- [ ] Categories seeded on first app launch
- [ ] Default categories marked as "system" (non-deletable but editable)
- [ ] Each has name, icon, and color

#### FR-CAT-002: Add Category
| Attribute | Description |
|-----------|-------------|
| ID | FR-CAT-002 |
| Priority | Must Have |
| Description | User can create custom categories |

**Inputs:**
| Field | Type | Required | Validation |
|-------|------|----------|------------|
| Name | Text | Yes | Unique, 1-50 characters |
| Icon | Selection | Yes | From predefined icon set |
| Color | Selection | Yes | From predefined color palette |

**Acceptance Criteria:**
- [ ] Name uniqueness validated
- [ ] Icon picker shows grid of available icons
- [ ] Color picker shows predefined palette
- [ ] Preview shown before save

#### FR-CAT-003: Edit Category
| Attribute | Description |
|-----------|-------------|
| ID | FR-CAT-003 |
| Priority | Must Have |
| Description | User can modify existing categories |

**Acceptance Criteria:**
- [ ] All fields editable
- [ ] Changes reflect in existing transactions immediately
- [ ] Cannot change name to existing category name

#### FR-CAT-004: Delete Category
| Attribute | Description |
|-----------|-------------|
| ID | FR-CAT-004 |
| Priority | Must Have |
| Description | User can delete custom categories |

**Acceptance Criteria:**
- [ ] Cannot delete category with associated transactions
- [ ] If transactions exist: offer reassignment dialog
- [ ] Reassignment dialog shows category picker
- [ ] After reassignment, category deleted
- [ ] System default categories cannot be deleted

#### FR-CAT-005: List Categories
| Attribute | Description |
|-----------|-------------|
| ID | FR-CAT-005 |
| Priority | Must Have |
| Description | User can view all categories |

**Acceptance Criteria:**
- [ ] Grid or list view of all categories
- [ ] Each shows icon, name, color indicator
- [ ] Transaction count per category shown
- [ ] Search/filter by name
- [ ] Tap to edit

---

### 3.3 Dashboard Module

#### FR-DASH-001: Month Selector
| Attribute | Description |
|-----------|-------------|
| ID | FR-DASH-001 |
| Priority | Must Have |
| Description | User can select which month to view |

**Acceptance Criteria:**
- [ ] Left/right arrows to navigate months
- [ ] Current month shown prominently
- [ ] Tap month to open month/year picker
- [ ] Default to current month on app open

#### FR-DASH-002: Summary Cards
| Attribute | Description |
|-----------|-------------|
| ID | FR-DASH-002 |
| Priority | Must Have |
| Description | Display key financial metrics for selected month |

**Metrics Displayed:**
| Metric | Calculation | Display |
|--------|-------------|---------|
| Total Expenses | Sum of expense transactions | Red/danger color |
| Total Income | Sum of income transactions | Green/accent color |
| Net Balance | Income - Expenses | Color based on +/- |
| Budget Remaining | Budget - Expenses | Progress indicator |

**Acceptance Criteria:**
- [ ] Cards update when month changes
- [ ] Amounts formatted with currency symbol
- [ ] Loading state while calculating
- [ ] Tap card for detailed breakdown (optional)

#### FR-DASH-003: Category Breakdown
| Attribute | Description |
|-----------|-------------|
| ID | FR-DASH-003 |
| Priority | Must Have |
| Description | Visual breakdown of spending by category |

**Acceptance Criteria:**
- [ ] Shows top 5-7 expense categories
- [ ] Each shows: icon, name, amount, percentage
- [ ] Progress bar or pie chart visualization
- [ ] "Other" category for remaining
- [ ] Tap category to see transactions in that category

#### FR-DASH-004: Recent Transactions
| Attribute | Description |
|-----------|-------------|
| ID | FR-DASH-004 |
| Priority | Must Have |
| Description | Show most recent transactions |

**Acceptance Criteria:**
- [ ] Show last 5-10 transactions
- [ ] Same format as transaction list items
- [ ] "See All" button to navigate to full list
- [ ] Tap item to edit

---

### 3.4 Budgets Module

#### FR-BUD-001: Set Monthly Budget
| Attribute | Description |
|-----------|-------------|
| ID | FR-BUD-001 |
| Priority | Should Have |
| Description | User can set overall monthly spending budget |

**Inputs:**
| Field | Type | Required |
|-------|------|----------|
| Amount | Decimal | Yes |
| Month | Month/Year | Yes |
| Carry Over | Boolean | No |

**Acceptance Criteria:**
- [ ] One overall budget per month
- [ ] Can set for current or future months
- [ ] Option to carry unused budget to next month

#### FR-BUD-002: Set Category Budget
| Attribute | Description |
|-----------|-------------|
| ID | FR-BUD-002 |
| Priority | Should Have |
| Description | User can set budget per category |

**Acceptance Criteria:**
- [ ] Budget for specific category for a month
- [ ] Sum of category budgets can exceed overall budget (warning shown)
- [ ] Categories without budget show "No limit"

#### FR-BUD-003: Budget Progress
| Attribute | Description |
|-----------|-------------|
| ID | FR-BUD-003 |
| Priority | Should Have |
| Description | Visual progress indicators for budgets |

**Acceptance Criteria:**
- [ ] Progress bar shows percentage spent
- [ ] Color coding: Green (0-79%), Amber (80-99%), Red (100%+)
- [ ] Shows: spent amount, remaining amount, percentage
- [ ] Updates in real-time as transactions added

#### FR-BUD-004: Budget Alerts
| Attribute | Description |
|-----------|-------------|
| ID | FR-BUD-004 |
| Priority | Could Have |
| Description | Notify user when approaching/exceeding budget |

**Alert Thresholds:**
- 80% reached: Warning notification
- 100% reached: Over-budget notification

**Acceptance Criteria:**
- [ ] In-app notification banner
- [ ] Optional push notification (if enabled)
- [ ] Alert shown once per threshold per budget

---

### 3.5 Accounts Module

#### FR-ACC-001: Default Accounts
| Attribute | Description |
|-----------|-------------|
| ID | FR-ACC-001 |
| Priority | Should Have |
| Description | App provides default account types |

**Default Accounts:**
| Name | Type | Icon |
|------|------|------|
| Cash | Cash | wallet |
| Bank Account | Bank | account_balance |
| Credit Card | Card | credit_card |

#### FR-ACC-002: Add Account
| Attribute | Description |
|-----------|-------------|
| ID | FR-ACC-002 |
| Priority | Should Have |
| Description | User can create additional accounts |

**Inputs:**
| Field | Type | Required |
|-------|------|----------|
| Name | Text | Yes |
| Type | Enum | Yes |
| Initial Balance | Decimal | No |
| Icon | Selection | No |
| Color | Selection | No |
| Is Default | Boolean | No |

#### FR-ACC-003: Account Balance
| Attribute | Description |
|-----------|-------------|
| ID | FR-ACC-003 |
| Priority | Should Have |
| Description | Track running balance per account |

**Calculation:**
```
Current Balance = Initial Balance + Income Transactions - Expense Transactions
```

**Acceptance Criteria:**
- [ ] Balance shown on account card
- [ ] Updates after each transaction
- [ ] Historical balance not tracked (current only)

---

### 3.6 Recurring Transactions Module

#### FR-REC-001: Create Recurring Transaction
| Attribute | Description |
|-----------|-------------|
| ID | FR-REC-001 |
| Priority | Could Have |
| Description | User can set up transactions that repeat |

**Inputs:**
| Field | Type | Required |
|-------|------|----------|
| All transaction fields | - | Same as transaction |
| Recurrence Type | Enum | Yes |
| Day of Month/Week | Number | Depends on type |
| Start Date | Date | Yes |
| End Date | Date | No |

**Recurrence Types:**
- Daily
- Weekly (select day)
- Bi-weekly
- Monthly (select day of month)
- Yearly

#### FR-REC-002: Auto-Generate Transactions
| Attribute | Description |
|-----------|-------------|
| ID | FR-REC-002 |
| Priority | Could Have |
| Description | System creates transactions on due dates |

**Acceptance Criteria:**
- [ ] Transactions generated when app opens (catch up)
- [ ] Generated transactions editable like regular transactions
- [ ] Link maintained to recurring source
- [ ] Notification when recurring transaction posted

#### FR-REC-003: Manage Recurring
| Attribute | Description |
|-----------|-------------|
| ID | FR-REC-003 |
| Priority | Could Have |
| Description | User can view, edit, pause, delete recurring items |

**Acceptance Criteria:**
- [ ] List of all recurring transactions
- [ ] Shows: description, amount, frequency, next due date
- [ ] Pause/resume toggle
- [ ] Edit updates future occurrences only
- [ ] Delete removes recurring (past transactions remain)

---

### 3.7 Reports Module

#### FR-RPT-001: Monthly Trend
| Attribute | Description |
|-----------|-------------|
| ID | FR-RPT-001 |
| Priority | Could Have |
| Description | View income vs expense trend over months |

**Acceptance Criteria:**
- [ ] Line or bar chart showing last 6 or 12 months
- [ ] Toggle between 6/12 month view
- [ ] Shows both income and expense lines
- [ ] Tap data point to see month details

#### FR-RPT-002: Category Trend
| Attribute | Description |
|-----------|-------------|
| ID | FR-RPT-002 |
| Priority | Could Have |
| Description | View spending trend per category |

**Acceptance Criteria:**
- [ ] Select one or more categories
- [ ] Shows spending over time for selected categories
- [ ] Compare categories side by side

#### FR-RPT-003: Export Report
| Attribute | Description |
|-----------|-------------|
| ID | FR-RPT-003 |
| Priority | Could Have |
| Description | Export transaction data |

**Export Formats:**
- CSV (transactions list)
- JSON (full backup including settings)

**Acceptance Criteria:**
- [ ] Select date range for export
- [ ] Choose format (CSV/JSON)
- [ ] Save to device storage
- [ ] Share via system share sheet

---

### 3.8 Settings Module

#### FR-SET-001: Currency Selection
| Attribute | Description |
|-----------|-------------|
| ID | FR-SET-001 |
| Priority | Must Have |
| Description | User can select display currency |

**Acceptance Criteria:**
- [ ] List of common currencies with symbol
- [ ] Selected currency used throughout app
- [ ] Currency symbol shown with all amounts
- [ ] Default: USD ($)

#### FR-SET-002: Theme Selection
| Attribute | Description |
|-----------|-------------|
| ID | FR-SET-002 |
| Priority | Must Have |
| Description | User can choose app theme |

**Options:**
- System (follow device setting)
- Light
- Dark

**Acceptance Criteria:**
- [ ] Theme applies immediately on selection
- [ ] Persists across app restarts
- [ ] All screens respect theme

#### FR-SET-003: Backup Data
| Attribute | Description |
|-----------|-------------|
| ID | FR-SET-003 |
| Priority | Must Have |
| Description | User can export full data backup |

**Acceptance Criteria:**
- [ ] Single button to create backup
- [ ] Backup includes: transactions, categories, accounts, budgets, settings
- [ ] JSON format
- [ ] Saved to device Downloads folder
- [ ] Filename includes date: ExpenseMate_Backup_2024-12-23.json

#### FR-SET-004: Restore Data
| Attribute | Description |
|-----------|-------------|
| ID | FR-SET-004 |
| Priority | Must Have |
| Description | User can import data from backup |

**Acceptance Criteria:**
- [ ] File picker to select JSON backup
- [ ] Validation of backup file structure
- [ ] Options: Replace all data OR Merge with existing
- [ ] Confirmation before restore
- [ ] Error handling for corrupt/invalid files

#### FR-SET-005: Reset Data
| Attribute | Description |
|-----------|-------------|
| ID | FR-SET-005 |
| Priority | Should Have |
| Description | User can delete all data and start fresh |

**Acceptance Criteria:**
- [ ] Double confirmation required
- [ ] Clears all user data
- [ ] Re-seeds default categories
- [ ] Settings reset to defaults

#### FR-SET-006: App Lock
| Attribute | Description |
|-----------|-------------|
| ID | FR-SET-006 |
| Priority | Could Have |
| Description | Protect app with PIN or biometric |

**Options:**
- 4-digit PIN
- Biometric (fingerprint/face if device supports)
- Both (PIN as fallback)

**Acceptance Criteria:**
- [ ] Lock screen shown on app open
- [ ] Configurable timeout (immediate, 1 min, 5 min)
- [ ] PIN change requires current PIN
- [ ] Biometric enrollment follows device settings

---

## 4. NON-FUNCTIONAL REQUIREMENTS

### 4.1 Performance

#### NFR-PERF-001: App Launch Time
- Cold start: < 3 seconds to interactive
- Warm start: < 1 second

#### NFR-PERF-002: List Performance
- Smooth scrolling at 60fps for lists up to 10,000 items
- Use virtualization for all lists

#### NFR-PERF-003: Data Operations
- Transaction save: < 500ms
- Dashboard load: < 1 second
- Report generation: < 3 seconds

### 4.2 Usability

#### NFR-USE-001: Touch Targets
- All interactive elements minimum 44x44 dp
- Adequate spacing between touch targets

#### NFR-USE-002: Accessibility
- Support screen readers (TalkBack/Narrator)
- Respect system font scaling
- Color contrast ratio minimum 4.5:1

#### NFR-USE-003: Feedback
- Loading indicators for all async operations
- Success/error messages for user actions
- Empty states with helpful guidance

### 4.3 Reliability

#### NFR-REL-001: Data Integrity
- No data loss on app crash
- Transactions saved immediately (no batch)
- Database integrity checks on startup

#### NFR-REL-002: Error Handling
- Graceful handling of all errors
- User-friendly error messages
- Automatic retry for recoverable errors

### 4.4 Security

#### NFR-SEC-001: Local Data
- All data stored locally only
- No network calls (except optional future sync)
- No analytics or tracking

#### NFR-SEC-002: Secure Storage
- Sensitive settings in secure storage
- PIN hash stored, not plain text
- Biometric uses device secure enclave

### 4.5 Maintainability

#### NFR-MAINT-001: Architecture
- Follow VanillaSlice vertical slice pattern
- SOLID principles
- Dependency injection throughout

#### NFR-MAINT-002: Code Quality
- Consistent naming conventions
- XML documentation for public APIs
- Unit tests for business logic

---

## 5. USER INTERFACE REQUIREMENTS

### 5.1 Navigation Structure

```
┌─────────────────────────────────────────────────┐
│                   App Header                    │
│              [Screen Title]    [Actions]        │
├─────────────────────────────────────────────────┤
│                                                 │
│                                                 │
│               Main Content Area                 │
│                                                 │
│                                                 │
│                                    [FAB: + Add] │
├──────────┬──────────┬──────────┬───────────────┤
│  🏠 Home │ 💳 Trans │ 📊 Budget│ ⚙️ Settings   │
│          │  actions │    s     │               │
└──────────┴──────────┴──────────┴───────────────┘
```

### 5.2 Screen Inventory

| Screen | Route | Parent Tab |
|--------|-------|------------|
| Dashboard | //home | Home |
| Transaction List | //transactions | Transactions |
| Transaction Form | //transactions/form | - |
| Category List | //settings/categories | Settings |
| Category Form | //settings/categories/form | - |
| Budget List | //budgets | Budgets |
| Budget Form | //budgets/form | - |
| Account List | //settings/accounts | Settings |
| Settings Main | //settings | Settings |
| Backup/Restore | //settings/backup | Settings |
| Reports | //reports | - |

### 5.3 Design Tokens

#### Colors
| Token | Light | Dark | Usage |
|-------|-------|------|-------|
| Primary | #2563EB | #3B82F6 | Buttons, links, active states |
| PrimaryDark | #1D4ED8 | #2563EB | Pressed states |
| Accent | #22C55E | #22C55E | Income, positive values |
| Danger | #EF4444 | #EF4444 | Expenses, errors, destructive |
| Warning | #F59E0B | #F59E0B | Budget warnings |
| Background | #F8FAFC | #0B1220 | Page backgrounds |
| Surface | #FFFFFF | #111827 | Cards, modals |
| TextPrimary | #0F172A | #E5E7EB | Main text |
| TextSecondary | #475569 | #94A3B8 | Labels, hints |
| Divider | #E2E8F0 | #1F2937 | Borders, separators |

#### Typography
| Style | Size | Weight | Line Height |
|-------|------|--------|-------------|
| DisplayLarge | 32sp | 600 | 40sp |
| DisplayMedium | 28sp | 600 | 36sp |
| TitleLarge | 22sp | 600 | 28sp |
| TitleMedium | 18sp | 600 | 24sp |
| BodyLarge | 16sp | 400 | 24sp |
| BodyMedium | 14sp | 400 | 20sp |
| BodySmall | 12sp | 400 | 16sp |
| LabelLarge | 14sp | 600 | 20sp |

#### Spacing
| Token | Value |
|-------|-------|
| xs | 4dp |
| sm | 8dp |
| md | 16dp |
| lg | 24dp |
| xl | 32dp |

#### Borders
| Token | Value |
|-------|-------|
| RadiusSm | 4dp |
| RadiusMd | 8dp |
| RadiusLg | 12dp |
| RadiusFull | 9999dp |

---

## 6. DATA REQUIREMENTS

### 6.1 Entity Relationship Diagram

```
┌─────────────┐     ┌─────────────┐
│  Category   │     │   Account   │
├─────────────┤     ├─────────────┤
│ Id (PK)     │     │ Id (PK)     │
│ Name        │     │ Name        │
│ Icon        │     │ Type        │
│ Color       │     │ Balance     │
│ IsDefault   │     │ IsDefault   │
│ IsActive    │     │ IsActive    │
└──────┬──────┘     └──────┬──────┘
       │                   │
       │    ┌──────────────┤
       │    │              │
       ▼    ▼              │
┌─────────────────┐        │
│   Transaction   │        │
├─────────────────┤        │
│ Id (PK)         │        │
│ Amount          │        │
│ Type            │        │
│ CategoryId (FK) │────────┘
│ AccountId (FK)  │
│ Date            │
│ Note            │
│ Tags            │
│ RecurringId(FK) │──────┐
│ CreatedAt       │      │
└─────────────────┘      │
                         │
┌─────────────────┐      │
│    Recurring    │◄─────┘
├─────────────────┤
│ Id (PK)         │
│ Amount          │
│ Type            │
│ CategoryId (FK) │
│ Frequency       │
│ NextDue         │
│ IsActive        │
└─────────────────┘

┌─────────────────┐
│     Budget      │
├─────────────────┤
│ Id (PK)         │
│ CategoryId (FK) │ (nullable = overall)
│ Amount          │
│ Month           │
│ Year            │
│ CarryOver       │
└─────────────────┘

┌─────────────────┐
│   AppSettings   │
├─────────────────┤
│ Id (PK)         │ (singleton, always 1)
│ Currency        │
│ WeekStartDay    │
│ DefaultAccount  │
│ Theme           │
│ UsePinLock      │
│ PinHash         │
└─────────────────┘
```

### 6.2 Data Validation Rules

| Entity | Field | Rule |
|--------|-------|------|
| Transaction | Amount | > 0 |
| Transaction | CategoryId | Must exist |
| Transaction | Date | Required |
| Category | Name | Unique, 1-50 chars |
| Budget | Amount | > 0 |
| Budget | Month/Year | Valid month (1-12), reasonable year |
| Account | Name | Unique, 1-50 chars |

---

## 7. CONSTRAINTS AND ASSUMPTIONS

### 7.1 Constraints
1. Must use existing VanillaSlice architecture
2. Must remain native XAML MAUI (no Blazor)
3. Must work completely offline
4. Must not introduce external API dependencies
5. Must support both light and dark themes

### 7.2 Assumptions
1. User has a single-user device (no multi-user support)
2. Transactions are in a single currency at a time
3. Historical exchange rates not tracked
4. No cloud sync in initial release
5. Device has adequate storage for SQLite database

---

## 8. ACCEPTANCE CRITERIA SUMMARY

### Phase 1 - MVP (Must Complete)
- [ ] User can add/edit/delete transactions
- [ ] User can view transactions grouped by date
- [ ] User can search and filter transactions
- [ ] User can manage categories (CRUD)
- [ ] Dashboard shows monthly summary
- [ ] Theme system working (light/dark)
- [ ] Data persists in SQLite

### Phase 2 - Core Features
- [ ] Budget setting and tracking
- [ ] Multiple accounts/wallets
- [ ] Recurring transactions
- [ ] Budget alerts

### Phase 3 - Pro Features
- [ ] Reports and trends
- [ ] Data backup/restore
- [ ] App lock (PIN/biometric)
- [ ] CSV export

---

## 9. REVISION HISTORY

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 1.0 | Dec 2024 | - | Initial release |

---

**END OF REQUIREMENTS DOCUMENT**
