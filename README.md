# 🏢 Condominium Package Management

Sistema de **gestão de encomendas para condomínios**, projetado com foco em **DDD, Event Sourcing e arquitetura moderna**, com potencial evolução para SaaS multi-tenant.

---

## 🚀 Objetivo

Este projeto foi desenvolvido com foco educacional para praticar:

- Domain-Driven Design (DDD)
- Event Sourcing
- CQRS (em evolução)
- Arquitetura limpa (Clean Architecture)
- Observabilidade com OpenTelemetry
- Integração com mensageria (Kafka)
- Deploy em Kubernetes

---

## 🧠 Domínio

### 📌 Domínio principal
**Gestão Condominial**

### 📦 Bounded Context atual
**Encomendas (Package Management)**

Responsável por:

- Recebimento de encomendas
- Armazenamento temporário
- Entrega ao morador
- Cancelamento
- Notificações
- Auditoria baseada em eventos

---

## 🧱 Modelo de Domínio

### Aggregates

- `Package` (Aggregate Root)
- `Resident`
- `Unit`
- `Condominium`

### Value Objects

- `PackageId`
- `ResidentId`
- `UnitId`
- `CondominiumId`
- `EventId`

---

## 📦 Aggregate: Package

Representa uma encomenda recebida na portaria.

### 📌 Atributos

- `PackageId`
- `ResidentId`
- `UnitId`
- `ReceivedAtUtc`
- `CollectedAtUtc`

---

## 🔄 Estados

```text
Received → Collected (final)
         → Cancelled (final)