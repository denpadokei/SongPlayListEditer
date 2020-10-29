﻿using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Components;
using BS_Utils.Utilities;
using HMUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SongPlayListEditer.BeatSaberCommon
{
    public static class BeatSaberUI
    {
        /// <summary>
        /// Clone a Unity Button into a Button we control.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="buttonTemplate"></param>
        /// <param name="buttonInstance"></param>
        /// <returns></returns>
        static public Button CreateUIButton(RectTransform parent, Button buttonTemplate, string name = "")
        {
            Button btn = UnityEngine.Object.Instantiate(buttonTemplate, parent);
            UnityEngine.Object.DestroyImmediate(btn.GetComponent<SignalOnUIButtonClick>());
            btn.onClick = new Button.ButtonClickedEvent();
            btn.name = string.IsNullOrEmpty(name) ? "CustomUIButton" : name;
            btn.interactable = true;
            Polyglot.LocalizedTextMeshProUGUI localizer = btn.GetComponentInChildren<Polyglot.LocalizedTextMeshProUGUI>();
            if (localizer != null)
                GameObject.Destroy(localizer);
            //CurvedTextMeshPro textMeshPro = btn.GetComponentInChildren<CurvedTextMeshPro>();
            //if (textMeshPro != null)
            //    GameObject.Destroy(textMeshPro);
            ExternalComponents externalComponents = btn.gameObject.AddComponent<ExternalComponents>();
            TextMeshProUGUI textMesh = btn.GetComponentInChildren<TextMeshProUGUI>();
            textMesh.richText = true;
            if (!string.IsNullOrEmpty(name)) {
                textMesh.text = name;
            }
            externalComponents.components.Add(textMesh);
            StackLayoutGroup stackLayoutGroup = btn.GetComponentInChildren<StackLayoutGroup>();
            if (stackLayoutGroup != null)
                externalComponents.components.Add(stackLayoutGroup);

            return btn;
        }

        /// <summary>
        /// Create an icon button, simple.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="buttonTemplate"></param>
        /// <param name="iconSprite"></param>
        /// <returns></returns>
        //public static Button CreateIconButton(RectTransform parent, Button buttonTemplate, Sprite iconSprite)
        //{
        //    Button newButton = BeatSaberUI.CreateUIButton(parent, buttonTemplate);
        //    newButton.interactable = true;

        //    RectTransform textRect = newButton.GetComponentsInChildren<RectTransform>(true).FirstOrDefault(c => c.name == "Text");
        //    if (textRect != null) {
        //        UnityEngine.Object.Destroy(textRect.gameObject);
        //    }

        //    newButton.SetButtonIcon(iconSprite);
        //    newButton.onClick.RemoveAllListeners();

        //    return newButton;
        //}

        /// <summary>
        /// Creates a copy of a template button and returns it.
        /// </summary>
        /// <param name="parent">The transform to parent the button to.</param>
        /// <param name="buttonTemplate">The name of the button to make a copy of. Example: "QuitButton", "PlayButton", etc.</param>
        /// <param name="anchoredPosition">The position the button should be anchored to.</param>
        /// <param name="sizeDelta">The size of the buttons RectTransform.</param>
        /// <param name="onClick">Callback for when the button is pressed.</param>
        /// <param name="buttonText">The text that should be shown on the button.</param>
        /// <param name="icon">The icon that should be shown on the button.</param>
        /// <returns>The newly created button.</returns>
        public static Button CreateUIButton(RectTransform parent, string buttonTemplate, Vector2 anchoredPosition, Vector2 sizeDelta, UnityAction onClick = null, string buttonText = "BUTTON", Sprite icon = null)
        {
            Button btn = UnityEngine.Object.Instantiate(Resources.FindObjectsOfTypeAll<Button>().Last(x => (x.name == buttonTemplate)), parent, false);
            btn.onClick = new Button.ButtonClickedEvent();
            if (onClick != null)
                btn.onClick.AddListener(onClick);
            btn.name = "CustomUIButton";

            (btn.transform as RectTransform).anchorMin = new Vector2(0.5f, 0.5f);
            (btn.transform as RectTransform).anchorMax = new Vector2(0.5f, 0.5f);
            (btn.transform as RectTransform).anchoredPosition = anchoredPosition;
            (btn.transform as RectTransform).sizeDelta = sizeDelta;

            btn.SetButtonText(buttonText);
            if (icon != null)
                btn.SetButtonIcon(icon);

            return btn;
        }

        /// <summary>
        /// Creates a copy of a template button and returns it.
        /// </summary>
        /// <param name="parent">The transform to parent the button to.</param>
        /// <param name="buttonTemplate">The name of the button to make a copy of. Example: "QuitButton", "PlayButton", etc.</param>
        /// <param name="anchoredPosition">The position the button should be anchored to.</param>
        /// <param name="onClick">Callback for when the button is pressed.</param>
        /// <param name="buttonText">The text that should be shown on the button.</param>
        /// <param name="icon">The icon that should be shown on the button.</param>
        /// <returns>The newly created button.</returns>
        public static Button CreateUIButton(RectTransform parent, string buttonTemplate, Vector2 anchoredPosition, UnityAction onClick = null, string buttonText = "BUTTON", Sprite icon = null)
        {
            Button btn = UnityEngine.Object.Instantiate(Resources.FindObjectsOfTypeAll<Button>().Last(x => (x.name == buttonTemplate)), parent, false);
            btn.onClick = new Button.ButtonClickedEvent();
            if (onClick != null)
                btn.onClick.AddListener(onClick);
            btn.name = "CustomUIButton";

            (btn.transform as RectTransform).anchorMin = new Vector2(0.5f, 0.5f);
            (btn.transform as RectTransform).anchorMax = new Vector2(0.5f, 0.5f);
            (btn.transform as RectTransform).anchoredPosition = anchoredPosition;

            btn.SetButtonText(buttonText);
            if (icon != null)
                btn.SetButtonIcon(icon);

            return btn;
        }


        /// <summary>
        /// Creates a copy of a template button and returns it.
        /// </summary>
        /// <param name="parent">The transform to parent the button to.</param>
        /// <param name="buttonTemplate">The name of the button to make a copy of. Example: "QuitButton", "PlayButton", etc.</param>
        /// <param name="onClick">Callback for when the button is pressed.</param>
        /// <param name="buttonText">The text that should be shown on the button.</param>
        /// <param name="icon">The icon that should be shown on the button.</param>
        /// <returns>The newly created button.</returns>
        public static Button CreateUIButton(RectTransform parent, string buttonTemplate, UnityAction onClick = null, string buttonText = "BUTTON", Sprite icon = null)
        {
            Button btn = UnityEngine.Object.Instantiate(Resources.FindObjectsOfTypeAll<Button>().Last(x => (x.name == buttonTemplate)), parent, false);
            btn.onClick = new Button.ButtonClickedEvent();
            if (onClick != null)
                btn.onClick.AddListener(onClick);
            btn.name = "CustomUIButton";
            Polyglot.LocalizedTextMeshProUGUI localizer = btn.GetComponentInChildren<Polyglot.LocalizedTextMeshProUGUI>();
            if (localizer != null)
                GameObject.Destroy(localizer);
            ExternalComponents externalComponents = btn.gameObject.AddComponent<ExternalComponents>();
            TextMeshProUGUI textMesh = btn.GetComponentInChildren<TextMeshProUGUI>();
            textMesh.richText = true;
            externalComponents.components.Add(textMesh);

            (btn.transform as RectTransform).anchorMin = new Vector2(0.5f, 0.5f);
            (btn.transform as RectTransform).anchorMax = new Vector2(0.5f, 0.5f);
            btn.SetButtonText(buttonText);
            if (icon != null)
                btn.SetButtonIcon(icon);
            return btn;
        }


        /// <summary>
        /// Creates a TextMeshProUGUI component.
        /// </summary>
        /// <param name="parent">Thet ransform to parent the new TextMeshProUGUI component to.</param>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="anchoredPosition">The position the button should be anchored to.</param>
        /// <returns>The newly created TextMeshProUGUI component.</returns>
        public static TextMeshProUGUI CreateText(RectTransform parent, string text, Vector2 anchoredPosition)
        {
            return CreateText(parent, text, anchoredPosition, new Vector2(60f, 10f));
        }

        /// <summary>
        /// Creates a TextMeshProUGUI component.
        /// </summary>
        /// <param name="parent">Thet transform to parent the new TextMeshProUGUI component to.</param>
        /// <param name="text">The text to be displayed.</param>
        /// <param name="anchoredPosition">The position the text component should be anchored to.</param>
        /// <param name="sizeDelta">The size of the text components RectTransform.</param>
        /// <returns>The newly created TextMeshProUGUI component.</returns>
        public static TextMeshProUGUI CreateText(RectTransform parent, string text, Vector2 anchoredPosition, Vector2 sizeDelta)
        {
            GameObject gameObj = new GameObject("CustomUIText");
            gameObj.SetActive(false);

            TextMeshProUGUI textMesh = gameObj.AddComponent<TextMeshProUGUI>();
            textMesh.font = UnityEngine.GameObject.Instantiate(Resources.FindObjectsOfTypeAll<TMP_FontAsset>().First(t => t.name == "Teko-Medium SDF No Glow"));
            textMesh.rectTransform.SetParent(parent, false);
            textMesh.text = text;
            textMesh.fontSize = 4;
            textMesh.color = Color.white;

            textMesh.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            textMesh.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            textMesh.rectTransform.sizeDelta = sizeDelta;
            textMesh.rectTransform.anchoredPosition = anchoredPosition;

            gameObj.SetActive(true);
            return textMesh;
        }

        /// <summary>
        /// Replace existing HoverHint on stat panel icons.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="name"></param>
        /// <param name="text"></param>
        public static void SetHoverHint(RectTransform button, string name, string text)
        {
            HoverHintController hoverHintController = Resources.FindObjectsOfTypeAll<HoverHintController>().First();
            DestroyHoverHint(button);
            var newHoverHint = button.gameObject.AddComponent<HoverHint>();
            newHoverHint.SetPrivateField("_hoverHintController", hoverHintController);
            newHoverHint.text = text;
            newHoverHint.name = name;
        }

        /// <summary>
        /// Safely destroy existing hoverhint.
        /// </summary>
        /// <param name="button"></param>
        public static void DestroyHoverHint(RectTransform button)
        {
            HoverHint currentHoverHint = button.GetComponentsInChildren<HMUI.HoverHint>().First();
            if (currentHoverHint != null) {
                UnityEngine.GameObject.DestroyImmediate(currentHoverHint);
            }
        }

        /// <summary>
        /// Adjust button text size.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="fontSize"></param>
        static public void SetButtonTextColor(Button button, Color color)
        {
            TextMeshProUGUI txt = button.GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "Text");
            if (txt != null) {
                txt.color = color;
            }
        }

        /// <summary>
        /// Adjust button border.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="color"></param>
        static public void SetButtonBorder(Button button, Color color)
        {
            Image img = button.GetComponentsInChildren<Image>().FirstOrDefault(x => x.name == "Stroke");
            if (img != null) {
                img.color = color;
            }
        }

        /// <summary>
        /// Adjust button border.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="color"></param>
        static public void SetButtonBorderActive(Button button, bool active)
        {
            Image img = button.GetComponentsInChildren<Image>().FirstOrDefault(x => x.name == "Stroke");
            if (img != null) {
                img.gameObject.SetActive(active);
            }
        }

        /// <summary>
        /// Find and adjust a stat panel item text fields.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        static public void SetStatButtonText(RectTransform rect, String text)
        {
            TextMeshProUGUI txt = rect.GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.name == "ValueText");
            if (txt != null) {
                txt.text = text;
            }
        }

        /// <summary>
        /// Find and adjust a stat panel item icon.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="icon"></param>
        static public void SetStatButtonIcon(RectTransform rect, Sprite icon)
        {
            Image img = rect.GetComponentsInChildren<Image>().FirstOrDefault(x => x.name == "Icon");
            if (img != null) {
                img.sprite = icon;
                img.color = Color.white;
            }
        }

        /// <summary>
        /// Create an icon button, simple.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="buttonTemplate"></param>
        /// <param name="iconSprite"></param>
        /// <returns></returns>
        public static Button CreateIconButton(String name, RectTransform parent, String buttonTemplate, Vector2 anchoredPosition, Vector2 sizeDelta, UnityAction onClick, Sprite icon)
        {
            Logger.Debug($"CreateIconButton({name}, {parent}, {buttonTemplate}, {anchoredPosition}, {sizeDelta}");
            Button btn = UnityEngine.Object.Instantiate(Resources.FindObjectsOfTypeAll<Button>().Last(x => (x.name == buttonTemplate)), parent, false);
            btn.name = name;
            btn.interactable = true;

            UnityEngine.Object.Destroy(btn.GetComponent<HoverHint>());
            GameObject.Destroy(btn.GetComponent<LocalizedHoverHint>());
            btn.gameObject.AddComponent<BeatSaberMarkupLanguage.Components.ExternalComponents>().components.Add(btn.GetComponentsInChildren<LayoutGroup>().First(x => x.name == "Content"));

            Transform contentTransform = btn.transform.Find("Content");
            GameObject.Destroy(contentTransform.Find("Text").gameObject);
            Image iconImage = new GameObject("Icon").AddComponent<ImageView>();
            iconImage.material = BeatSaberMarkupLanguage.Utilities.ImageResources.NoGlowMat;
            iconImage.rectTransform.SetParent(contentTransform, false);
            iconImage.rectTransform.sizeDelta = new Vector2(10f, 10f);
            iconImage.sprite = icon;
            iconImage.preserveAspect = true;
            if (iconImage != null) {
                BeatSaberMarkupLanguage.Components.ButtonIconImage btnIcon = btn.gameObject.AddComponent<BeatSaberMarkupLanguage.Components.ButtonIconImage>();
                btnIcon.image = iconImage;
            }

            GameObject.Destroy(btn.transform.Find("Content").GetComponent<LayoutElement>());
            btn.GetComponentsInChildren<RectTransform>().First(x => x.name == "Underline").gameObject.SetActive(false);

            ContentSizeFitter buttonSizeFitter = btn.gameObject.AddComponent<ContentSizeFitter>();
            buttonSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            buttonSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

            (btn.transform as RectTransform).anchorMin = new Vector2(0.5f, 0.5f);
            (btn.transform as RectTransform).anchorMax = new Vector2(0.5f, 0.5f);
            (btn.transform as RectTransform).anchoredPosition = anchoredPosition;
            (btn.transform as RectTransform).sizeDelta = sizeDelta;

            btn.onClick.RemoveAllListeners();
            if (onClick != null)
                btn.onClick.AddListener(onClick);

            return btn;
        }
    }
}
