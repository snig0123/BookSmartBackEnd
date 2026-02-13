# BookSmart API Documentation

Base URLs: `https://localhost:7200` | `http://localhost:5083`

## Authentication

JWT Bearer tokens using HMAC SHA512. Include the token in the `Authorization` header:

```
Authorization: Bearer <token>
```

### Roles

| Role    | Description                          |
|---------|--------------------------------------|
| Client  | Default role assigned on registration |
| Staff   | Can manage services and appointments |
| Admin   | Can create staff accounts            |

---

## Endpoints

### User

#### `POST /User/Register`

Register a new user account. Automatically assigned the Client role.

**Auth:** None

**Request body:**

```json
{
  "forename": "string",
  "surname": "string",
  "email": "string",
  "password": "string"
}
```

**Responses:** `200 OK`

---

#### `GET /User/Login`

Authenticate and receive a JWT token.

**Auth:** None

**Query parameters:**

| Parameter  | Type   | Required |
|------------|--------|----------|
| `email`    | string | Yes      |
| `password` | string | Yes      |

**Responses:**

- `200 OK`

```json
{
  "token": "string"
}
```

- `401 Unauthorized` — Invalid credentials

---

#### `GET /User/GetUserProfile`

Get a user's profile information.

**Auth:** Bearer token (any authenticated user)

**Query parameters:**

| Parameter | Type | Required |
|-----------|------|----------|
| `userId`  | guid | Yes      |

**Responses:**

- `200 OK`

```json
{
  "forename": "string",
  "surname": "string",
  "roles": ["string"]
}
```

- `404 Not Found` — User does not exist

---

### Staff

#### `POST /Staff/Create`

Create a new staff user account. Requires Admin role.

**Auth:** Bearer token (Admin policy)

**Request body:**

```json
{
  "forename": "string",
  "surname": "string",
  "email": "string",
  "password": "string"
}
```

**Responses:** `200 OK`

---

### Service

#### `POST /Service/CreateService`

Create a new service for a staff member's business.

**Auth:** Bearer token (Staff policy)

**Request body:**

```json
{
  "staffUserId": "guid",
  "name": "string",
  "description": "string | null",
  "duration": "integer (minutes)",
  "price": "decimal"
}
```

**Responses:** `200 OK`

---

#### `GET /Service/GetService`

Get a single service by ID. Only returns non-deleted services.

**Auth:** None

**Query parameters:**

| Parameter   | Type | Required |
|-------------|------|----------|
| `serviceId` | guid | Yes      |

**Responses:**

- `200 OK`

```json
{
  "serviceId": "guid",
  "name": "string",
  "description": "string | null",
  "duration": "integer",
  "price": "decimal",
  "active": "boolean"
}
```

- `404 Not Found` — Service does not exist or has been deleted

---

#### `GET /Service/GetServicesByBusiness`

Get all active (non-deleted) services for a business.

**Auth:** None

**Query parameters:**

| Parameter    | Type | Required |
|--------------|------|----------|
| `businessId` | guid | Yes      |

**Responses:**

- `200 OK`

```json
[
  {
    "serviceId": "guid",
    "name": "string",
    "description": "string | null",
    "duration": "integer",
    "price": "decimal",
    "active": "boolean"
  }
]
```

---

#### `PUT /Service/UpdateService`

Update an existing service.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter   | Type | Required |
|-------------|------|----------|
| `serviceId` | guid | Yes      |

**Request body:**

```json
{
  "staffUserId": "guid",
  "name": "string",
  "description": "string | null",
  "duration": "integer",
  "price": "decimal"
}
```

**Responses:** `200 OK`

---

#### `DELETE /Service/DeleteService`

Soft delete a service. Sets the service as deleted and inactive.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter   | Type | Required |
|-------------|------|----------|
| `serviceId` | guid | Yes      |

**Responses:** `200 OK`

---

### Schedule

#### `POST /Schedule/CreateSchedule`

Create a new schedule slot for a staff member. Validates the user has the Staff role.

**Auth:** Bearer token (Staff policy)

**Request body:**

```json
{
  "staffUserId": "guid",
  "dayOfWeek": "integer (0 = Sunday, 6 = Saturday)",
  "startTime": "TimeOnly (HH:mm:ss)",
  "endTime": "TimeOnly (HH:mm:ss)"
}
```

**Responses:** `200 OK`

---

#### `GET /Schedule/GetSchedule`

Get a single schedule slot by ID. Only returns non-deleted schedules.

**Auth:** None

**Query parameters:**

| Parameter    | Type | Required |
|--------------|------|----------|
| `scheduleId` | guid | Yes      |

**Responses:**

- `200 OK`

```json
{
  "scheduleId": "guid",
  "staffUserId": "guid",
  "dayOfWeek": "integer",
  "startTime": "HH:mm:ss",
  "endTime": "HH:mm:ss",
  "active": "boolean"
}
```

- `404 Not Found` — Schedule does not exist or has been deleted

---

#### `GET /Schedule/GetSchedulesByStaff`

Get all active (non-deleted) schedule slots for a staff member. Clients can use this to see a staff member's availability.

**Auth:** None

**Query parameters:**

| Parameter     | Type | Required |
|---------------|------|----------|
| `staffUserId` | guid | Yes      |

**Responses:**

- `200 OK`

```json
[
  {
    "scheduleId": "guid",
    "staffUserId": "guid",
    "dayOfWeek": "integer",
    "startTime": "HH:mm:ss",
    "endTime": "HH:mm:ss",
    "active": "boolean"
  }
]
```

---

#### `PUT /Schedule/UpdateSchedule`

Update an existing schedule slot.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter    | Type | Required |
|--------------|------|----------|
| `scheduleId` | guid | Yes      |

**Request body:**

```json
{
  "staffUserId": "guid",
  "dayOfWeek": "integer (0 = Sunday, 6 = Saturday)",
  "startTime": "TimeOnly (HH:mm:ss)",
  "endTime": "TimeOnly (HH:mm:ss)"
}
```

**Responses:** `200 OK`

---

#### `DELETE /Schedule/DeleteSchedule`

Soft delete a schedule slot. Sets the schedule as deleted and inactive.

**Auth:** Bearer token (Staff policy)

**Query parameters:**

| Parameter    | Type | Required |
|--------------|------|----------|
| `scheduleId` | guid | Yes      |

**Responses:** `200 OK`

---

### Appointment

#### `POST /Appointment/Create`

Create a new appointment.

**Auth:** None

**Request body:**

```json
{
  "appt_DATE": "datetime (ISO 8601)",
  "appt_COMMENT": "string | null"
}
```

**Responses:** `201 Created`