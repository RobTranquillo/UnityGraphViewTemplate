# SceneFlow

Was soll ScenFlow machen?

Eine einfache Möglichkeit bieten, um den Ablauf von verschiedenen Szenen nacheinander und gleichzeitig zu steuern.
Basierend auf Addressables und ScriptableObjects.

In einer Init Scene werden ScriptableObjects verknüpft die den Ablauf der Szenen steuern.

Es braucht einen Trigger der die nächste Scene startet.


# SceneFlow-Node
Eine Node ist die kleineste Steuerungseinheit in SceneFlow.
Sie enthält die zuladenenden Scenen, den Trigger um gestartet zu werden und 