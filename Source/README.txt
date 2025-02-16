
=============================================
📌 CMSJet - Overview
=============================================
CMSJet is a cloud-based SaaS platform designed for **CMS-to-CMS content migration** with a 
**highly flexible, scalable, and secure infrastructure**. It enables users to migrate, 
transform, and map content between different CMS platforms while offering **real-time progress 
indicators, priority-based batch execution, retry mechanisms, and audit logs for compliance**.

Key Features:
- **Supports multiple CMS platforms** (Sitecore, WordPress, Drupal, Umbraco, etc.)
- **Automated migration workflows** with real-time execution tracking
- **Batch-Based Execution**: A **Migration** contains multiple **Batches**, each handling one content type.
- **Batches sorted by priority inside each Migration**
- **Flexible Mapping System**: 
  - Users can map **a single source field to multiple target fields**.
  - **Rules & Transformations**: Skip mapping based on conditions (e.g., "Don’t map if value contains X").
  - Apply **transformations** (e.g., **Replace**, **Remove**, **Truncate**, **Modify Date Format**).
- **Batch-Level Content Selection & Filtering**:
  - Users select **source type** → see available **content items** → apply filters.
  - Filters support **date ranges, keywords, status-based selection (e.g., "Published only")**.
- **Retry mechanism for failed migrations** (up to user-defined limits).
- **Batch prioritization** (users can manually set migration priority levels).
- **Enterprise-Level Security** (OAuth2, RBAC, end-to-end encryption).

=============================================
🖥️ 1. User Interface (UI) Wireframe - Text-Based
=============================================

### 🌍 1️⃣ Migration Management Screen
+------------------------------------------------------------------------------------------------+
| 📍 CMSJet Migrations Dashboard                                                                |
+------------------------------------------------------------------------------------------------+
| 🔍 [ Search Migrations... ]  [🔽 Filters]  [➕ Add New Migration]                              |
+------------------------------------------------------------------------------------------------+
|  ✅ Migration Name      | 🔄 Status    | 📅 Created On   | ⏳ Duration  | Priority  | Actions  |
|------------------------------------------------------------------------------------------------|
|  Sitecore → Drupal  | ✅ Completed  | 2025-02-12   | 12m 32s   | 🔼 High   | [📝 View] [🔄 Retry] [❌ Delete] |
|  WordPress → Sitecore | ❌ Failed    | 2025-02-10   |  5m 12s   | 🔽 Low    | [📝 View] [🔄 Retry] [❌ Delete] |
|  Umbraco → WP      | ⏳ In Progress | 2025-02-08   |  --       | ⏳ Normal | [📝 View] [🔄 Cancel] [❌ Delete] |
+------------------------------------------------------------------------------------------------+

### 🔄 2️⃣ Migration Setup - Top-Level Configuration
+------------------------------------------------------------------------------------------------+
| [🔙 Back]                           ➡ Migration Setup (Step 1/3)                               |
+------------------------------------------------------------------------------------------------+
|  📛 **Migration Name:**  [User Input]                                                          |
|  🔽 **Select Source CMS Connection:** [ Sitecore ]                                             |
|  🔽 **Select Target CMS Connection:** [ Drupal ]                                              |
|  ➡ Next Step: Batch Creation                                                                  |
+------------------------------------------------------------------------------------------------+

=============================================
📂 2. Batch Configuration & Rules
=============================================

### 🛠 4️⃣ Batch Creation (Each Batch is One Content Type)
+------------------------------------------------------------------------------------------------+
| 📂 **Batch List (Sorted by Priority)**                                                       |
|------------------------------------------------------------------------------------------------|
| 🟢 Batch #1 (Priority: High)  | Content Type: "News Article" | [⚙ Edit] [❌ Delete]           |
| 🟡 Batch #2 (Priority: Medium) | Content Type: "Media Files" | [⚙ Edit] [❌ Delete]           |
| 🔴 Batch #3 (Priority: Low)    | Content Type: "Events"      | [⚙ Edit] [❌ Delete]           |
+------------------------------------------------------------------------------------------------+
| [➕ Add New Batch]  [➡ Next Step: Mapping]                                                   |
+------------------------------------------------------------------------------------------------+

### 🔗 5️⃣ Field Mapping & Transformations
+------------------------------------------------------------------------------------------------+
| 📂 **Batch: "News Article" → "Blog Post"**                                                  |
+------------------------------------------------------------------------------------------------+
| Source Field  | Target Field(s)  | Transformations Applied            | Actions               |
|------------------------------------------------------------------------------------------------|
| Title        | Heading, SEO Title | [🔄 Replace "CMS" → "New Tech"]   | [📝 Edit] [❌ Remove]   |
| Body         | Content            | [🔄 Remove HTML] [🔄 Truncate]     | [📝 Edit] [❌ Remove]   |
| Image URL    | Featured Image     | [🔄 Resize (800px)] [🔄 Convert WebP] | [📝 Edit] [❌ Remove] |
+------------------------------------------------------------------------------------------------+
| [➕ Add Field Mapping]  [➡ Next: Content Selection]                                          |
+------------------------------------------------------------------------------------------------+

### 🛑 6️⃣ Batch-Level Rules
+------------------------------------------------------------------------------------------------+
| 📂 **Define Rules to Exclude Content Items from Migration**                                   |
|------------------------------------------------------------------------------------------------|
| Rule Name                 | Field Affected | Condition              | Value          | Actions |
|------------------------------------------------------------------------------------------------|
| Skip Promotional Content  | Title         | Contains               | "Sponsored"    | [❌ Remove] |
| Ignore Old Content        | Created Date  | Before                 | "2020-01-01"   | [❌ Remove] |
| Remove Internal Notes     | Body          | Contains (Case-Sensitive) | "[INTERNAL]" | [❌ Remove] |
+------------------------------------------------------------------------------------------------+
| [➕ Add Rule]  [💾 Save Batch Rules] [🚀 Start Migration]                                     |
+------------------------------------------------------------------------------------------------+

=============================================
🔄 3. CMSJet Data Flow Architecture
=============================================
1️⃣ **User creates Migration & Batches via UI.**  
↓  
2️⃣ **CMS Content Extraction:**  
   - Fetches content via API (or Plugin if behind firewall).  
   - **Users can select & filter content items inside batches.**  
↓  
3️⃣ **Task Queue (Azure Service Bus):**  
   - Migration jobs are queued asynchronously.  
   - **Users manually set batch priority levels.**  
   - Failed tasks are moved to **Retry Queue** (max 3 retries).  
↓  
4️⃣ **Azure Functions Process Migration:**  
   - Applies field mappings & transformations.  
   - Updates **real-time progress indicators** via WebSockets.  
   - Logs execution to PostgreSQL + Table Storage.  
↓  
5️⃣ **Migration Data Sent to Target CMS**  
   - If **CMS API is available**, data is sent directly.  
   - If **CMS Plugin is required**, content is uploaded to Azure Blob → Plugin imports it.  
↓  
6️⃣ **Migration Completed → Logs & Notifications**  
   - User gets **email or Slack notifications**.  
   - Logs are stored in **Azure Table Storage**.  
   - Users can **download logs in CSV/JSON/PDF**.  
↓  
7️⃣ **Audit Logs & Post-Migration Review**  
   - Every migration attempt is **audited** for compliance.  
   - Users can **review detailed logs, retry failures, and export reports**.  
