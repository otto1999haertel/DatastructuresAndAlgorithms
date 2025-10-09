# Threading in C#

## 1. Grundlagen

- **Thread**: Die kleinste Ausführungseinheit, die vom Betriebssystem mit Prozessorzeit versehen wird. Ein Thread ist eine Abfolge von Programmanweisungen, die unabhängig von einem Scheduler verwaltet werden.
- **Single-Threaded Programm**: Nur ein Thread hat Zugriff auf den gesamten Prozess.
- **Multi-Threaded Programm**: Mehrere Threads existieren innerhalb eines Prozesses. Threads laufen unabhängig, teilen sich jedoch die Ressourcen des Prozesses (z. B. Speicher).
- **Gemeinsame Ressourcen**: Threads nutzen gemeinsam Speicher und andere Ressourcen. Nicht-atomare Operationen (z. B. Inkrementieren einer Variable) können durch parallele Threads unterbrochen werden, was zu **Race Conditions** führt.
- **Thread-Scheduler**: Das Betriebssystem verteilt Prozessorzeit an Threads basierend auf Prioritäten und Scheduling-Algorithmen.

### Szenarien für Threading
- **Berechnungsintensive Aufgaben**: Trennung von rechenintensivem Code und UI-Code in separate Threads.
- **Divide-and-Conquer-Algorithmen**: Aufgaben parallel auf mehrere Threads aufteilen.
- **I/O-bound Operationen**: Asynchrone Verarbeitung von Datei-, Datenbank- oder Netzwerkzugriffen.

## 2. Threads und Ressourcen

- **Thread-Lokaler Speicher**: Die CLR (Common Language Runtime) weist jedem Thread einen eigenen Speicher-Stack zu, um lokale Variablen getrennt zu halten. Jeder Thread hat eine eigene Kopie lokaler Variablen.
  - **ThreadLocal<T>**: Ermöglicht thread-spezifische Daten, die nicht automatisch kopiert werden.
- **Gemeinsame Ressourcen**: Threads teilen sich den Prozessspeicher, was Synchronisation erfordert, um Konflikte wie Race Conditions zu vermeiden.
- **Overhead**: Jeder Thread verursacht Zeit- und Speicheroverhead. Zu viele Threads können zu Kontextwechseln und Performance-Einbußen führen.
- **Thread-Prioritäten**: Threads können Prioritäten (`Thread.Priority`) zugewiesen werden, um die Reihenfolge der Ausführung zu beeinflussen (z. B. `ThreadPriority.Lowest` oder `ThreadPriority.Highest`).

## 3. Thread-Pools

- **Thread-Pool**: Reduziert Overhead durch Wiederverwendung und Recycling von Threads. Threads im Pool sind Hintergrund-Threads.
- **Hintergrund-Threads**:
  - Identisch zu Vordergrund-Threads, halten die Anwendung jedoch nicht am Laufen, wenn der Hauptthread endet.
  - Gekennzeichnet durch die `IsBackground`-Eigenschaft.
- **Thread-Pool-Eigenschaften**:
  - Begrenzt die Anzahl gleichzeitig laufender Threads. Überschüssige Aufgaben werden in einer Warteschlange gespeichert.
  - `Thread.CurrentThread.IsThreadPoolThread` prüft, ob ein Thread aus dem Thread-Pool stammt.
  - Konfigurierbar, z. B. durch `ThreadPool.SetMaxThreads` für maximale Thread-Anzahl.
- **Vorteil**: Thread-Pools verbessern die Performance bei vielen kurzen Aufgaben, da die Erstellung neuer Threads vermieden wird.

## 4. Synchronisation

Synchronisation ist notwendig, um bei mehreren Threads vorhersehbare Ergebnisse zu gewährleisten und Race Conditions oder Deadlocks zu vermeiden.

### Synchronisationsmechanismen

- **Blocking-Methoden**:
  - `Thread.Sleep`: Pausiert den Thread für eine bestimmte Zeit (nicht für Synchronisation empfohlen).
  - `Thread.Join`: Blockiert den aufrufenden Thread, bis der Ziel-Thread beendet ist.
  - `Task.Wait`: Wartet, bis eine Aufgabe abgeschlossen ist.
- **Locks**:
  - **Exklusive Locks**: Nur ein Thread darf den Code ausführen.
    - `lock`: Leichtgewichtige Synchronisation innerhalb eines Prozesses. => kann mittels/ auf Objekten angewendet werden
    - `Mutex`: Globale Synchronisation über mehrere Prozesse hinweg (ressourcenintensiv).
  - **Monitor**: Arbeitet auf Objektebene, ähnlich wie `lock`, bietet aber erweiterte Funktionen (z. B. Zeitlimits mit `Monitor.TryEnter`).
  - **Nicht-exklusive Locks**:
    - `Semaphore`: Begrenzt die Anzahl von Threads, die auf eine Ressource zugreifen können.
    - `ReaderWriterLockSlim`: Ermöglicht mehreren Threads Lesezugriff, aber exklusiven Schreibzugriff.
- **Signaling**:
  - **EventWaitHandle**: Ermöglicht, dass ein Thread pausiert, bis ein Signal von einem anderen Thread empfangen wird (z. B. `AutoResetEvent`, `ManualResetEvent`).
  - Sinnvoll, wenn ein Thread auf das Ergebnis eines anderen Threads warten muss.
- **Interlocked**: Atomare Operationen für einfache Synchronisation (z. B. `Interlocked.Increment`, `Interlocked.CompareExchange`).
- **Volatile und Memory Barriers**: Die `volatile`-Schlüsselwort oder `Volatile`-Klasse stellt sicher, dass Speicherzugriffe in der richtigen Reihenfolge erfolgen.
- **Non-Blocking Constructs**: Thread-sichere Kollektionen wie `ConcurrentQueue`, `ConcurrentDictionary` oder `ConcurrentBag` vermeiden Blockierungen.

### Risiken der Synchronisation

- **Deadlocks**: Treten auf, wenn Threads sich gegenseitig blockieren (z. B. durch verschachtelte Locks). Vermeidbar durch:
  - Konsistente Reihenfolge beim Erwerb von Locks.
  - Verwendung von `Monitor.TryEnter` mit Zeitlimits.
  - Vermeidung unnötiger verschachtelter Locks.
- **Performance-Overhead**: Synchronisation (insbesondere `Mutex`) kann teuer sein. Wähle die leichtgewichtigste Methode (z. B. `Interlocked` für einfache Operationen).

## 5. Tasks und Async/Await

- **Tasks**: Eine höhere Abstraktionsebene im Vergleich zu Threads, vereinfacht die Erstellung und Verwaltung paralleler Aufgaben (Task Parallel Library, TPL).
  - Nutzen häufig den Thread-Pool.
  - Können Werte zurückgeben (`Task<T>`).
  - Besonders nützlich für I/O-bound Operationen (z. B. Netzwerk- oder Dateizugriffe).
  - Können zu Listen hinzugefügt werden und anschließend mit Task.WhenAll() parallel abgearbeitet werden inkl. auf Warten der Ausführung
  - Modernen/ Leichtgewichtiger als Threads => bevorzugte Nutzung
- **Task.Factory und TaskCreationOptions**:
  - `Task.Factory` ermöglicht die Erstellung von Tasks mit spezifischen Optionen (z. B. `TaskCreationOptions.LongRunning` für langlaufende Aufgaben).
- **Async/Await**:
  - Ermöglicht asynchrone Programmierung, bei der der Thread während des Wartens auf I/O freigegeben wird.
  - `await Task`: Der aktuelle Thread wartet asynchron, bis die Aufgabe abgeschlossen ist.
- **Continuation Tasks**:
  - Ermöglicht die Verkettung von Aufgaben, bei denen die Ausgabe einer Aufgabe als Eingabe für die nächste dient (z. B. `Task.ContinueWith`).
- **Asynchrone Streams**:
  - `IAsyncEnumerable` (ab .NET Core 2.1) ermöglicht die Verarbeitung von Datenströmen asynchron mit `await foreach`.
- **ValueTask**: Eine performantere Alternative zu `Task` für Szenarien mit häufigem asynchronem Zugriff.
- **Callbacks**: Traditionelle Methode zur Ausführung von Code nach Abschluss einer Aufgabe. In modernem C# oft durch `async/await` ersetzt.
- **Out-of-Process-Aufrufe**: Bei API- oder Webserver-Anfragen können lokale Ressourcen freigegeben werden, während auf die Antwort gewartet wird.
- **Fehlerbehandlung**: Tasks werfen `AggregateException`, wenn mehrere Fehler auftreten (z. B. bei `Task.WhenAll`).

## 6. Parallel Programming

- **Parallel-Klasse**:
  - `Parallel.For` und `Parallel.ForEach`: Vereinfachen parallele Schleifen.
  - `Parallel.Invoke`: Führt mehrere Aktionen parallel aus.
  - Automatische Nutzung des Thread-Pools mit integrierter Lastverteilung.
- **PLINQ (Parallel LINQ)**:
  - Erweitert LINQ für parallele Datenverarbeitung (z. B. `AsParallel()`).
  - Ideal für datenintensive Operationen wie Aggregationen oder Filterungen.

## 7. Cancellation

- **CancellationToken**: Ermöglicht kontrolliertes Abbrechen von Tasks oder Threads.
  - Wird über `CancellationTokenSource` erstellt.
  - Kann an Tasks übergeben werden, um Abbruch zu signalisieren (z. B. `Task.Run(..., cancellationToken)`).
- **Anwendung**: Verhindert Ressourcenverschwendung bei abgebrochenen Operationen (z. B. Benutzerabbruch in einer GUI).

## 8. GUI-Programmierung und Threading

- **Regeln für GUI-Anwendungen**:
  - Führe keine langlaufenden Aufgaben auf dem UI-Thread aus, um Responsivität zu gewährleisten.
  - Greife nur vom UI-Thread auf UI-Elemente zu (z. B. in WPF via `Dispatcher.Invoke`).
- **Lösung**: Verwende `async/await` oder `Task.Run` für Hintergrundaufgaben.
  - Beispiel: `await Task.Run(() => LongRunningOperation());`

## 9. Thread-Sicherheit

- **Thread-Safe**: Gemeinsame Datenstrukturen müssen so gestaltet sein, dass alle Threads korrekt arbeiten, ohne unerwartete Interaktionen.
- **Maßnahmen**:
  - Verwende Synchronisationsmechanismen wie `lock`, `Interlocked` oder `Monitor`.
  - Nutze thread-sichere Kollektionen (`ConcurrentQueue`, `ConcurrentDictionary`, `ConcurrentBag`).
  - Vermeide unsynchronisierte Zugriffe auf gemeinsame Ressourcen.
- **Ziel**: Vorhersehbarer Programmablauf ohne Race Conditions.

## 10. Moderne Concurrency Patterns

- **Channels**:
  - Ermöglichen Producer-Consumer-Szenarien mit asynchroner Datenübergabe (z. B. `System.Threading.Channels`).
  - Ideal für Streaming-Daten oder Nachrichtenwarteschlangen.
- **Actor-Model**:
  - Mit Bibliotheken wie Akka.NET für verteilte, nachrichtenbasierte Concurrency.
- **TaskScheduler**:
  - Steuert, wie Tasks auf Threads verteilt werden (z. B. benutzerdefinierte Scheduler für UI-Threads).

## 11. Performance-Betrachtungen

- **Kontextwechsel**: Häufige Wechsel zwischen Threads können Performance beeinträchtigen.
- **Thread-Pool-Optimierung**: Passe die Größe des Thread-Pools an die Hardware an (`ThreadPool.SetMaxThreads`).
- **Messung**: Verwende `Stopwatch` oder Profiler-Tools, um Threading-Overhead zu analysieren.
- **Task vs. Thread**: Tasks sind oft performanter, da sie den Thread-Pool nutzen und weniger Overhead verursachen.

## 12. Best Practices

- **Vermeide manuelles Thread-Management**: Nutze `Task` und `async/await` statt `Thread` für moderne Anwendungen.
- **Thread-Pool bevorzugen**: Reduziert Overhead bei vielen kurzen Aufgaben.
- **Synchronisation gezielt einsetzen**: Wähle die leichtgewichtigste Methode (z. B. `Interlocked` statt `Mutex`).
- **Cancellation unterstützen**: Verwende `CancellationToken` für kontrollierte Abbrüche.
- **Debugging**: Nutze Visual Studio oder Rider, um Thread-Zustände, Deadlocks und Race Conditions zu analysieren.
- **Performance testen**: Verwende `Stopwatch` oder Profiler, um Threading-Effizienz zu messen.
- **Vermeide veraltete APIs**: Nutze keine `Thread.Abort`-Methode, da sie unsicher ist.

## 13. Ressourcen

- **Microsoft Learn**: Offizielle Dokumentation zu Threading und asynchroner Programmierung [](https://learn.microsoft.com/de-de/dotnet/csharp/asynchronous-programming/).
- **Bücher**:
  - "Concurrency in C# Cookbook" von Stephen Cleary.
  - "C# in Depth" von Jon Skeet (Kapitel zu Concurrency).
- **Online-Kurse**:
  - Pluralsight: "C# Concurrency and Multithreading".
  - Udemy: Kurse zu C# Multithreading.
  - YouTube: Tutorials von Kanälen wie "IAmTimCorey" oder "dotNET".
- **Tools**: Visual Studio (Thread-Debugger) oder JetBrains Rider.

## Beispielcode

### Einfacher Thread mit Lock
```csharp
using System;
using System.Threading;

class Program
{
    static int counter = 0;
    static readonly object lockObject = new object();

    static void Main()
    {
        Thread t1 = new Thread(IncrementCounter);
        Thread t2 = new Thread(IncrementCounter);
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
        Console.WriteLine($"Finaler Counter: {counter}"); // Sollte 2000 sein
    }

    static void IncrementCounter()
    {
        for (int i = 0; i < 1000; i++)
        {
            lock (lockObject)
            {
                counter++;
            }
        }
    }
}