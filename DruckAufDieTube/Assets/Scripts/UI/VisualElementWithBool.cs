using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class VisualElementWithBool : VisualElement
{
    public new class UxmlFactory : UxmlFactory<VisualElementWithBool, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlBoolAttributeDescription m_bool =
            new UxmlBoolAttributeDescription { name = "bool-attr", defaultValue = false };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as VisualElementWithBool;

            ate.boolAttr = m_bool.GetValueFromBag(bag, cc);
        }
    }

    public bool boolAttr { get; set; }
}