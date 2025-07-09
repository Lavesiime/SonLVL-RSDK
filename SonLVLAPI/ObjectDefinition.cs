using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

namespace SonicRetro.SonLVL.API
{
	public class ObjectData
	{
		[IniName("codefile")]
		public string CodeFile;
		[IniName("codetype")]
		public string CodeType;
		[IniName("xmlfile")]
		public string XMLFile;
		[IniName("name")]
		[DefaultValue("Unknown")]
		public string Name;
		[IniName("pri")]
		public bool Priority;
		[IniName("sheet")]
		public string Sheet;
		[IniName("frame")]
		public SpriteFrame Frame;
		[IniName("anim")]
		public AnimData Anim;
		[IniName("defaultsubtype")]
		[TypeConverter(typeof(ByteHexConverter))]
		public byte DefaultSubtype;
		[IniName("debug")]
		public bool Debug;
		[IniName("hidden")]
		public bool Hidden;
		[IniName("subtypes")]
		[IniCollection(IniCollectionMode.SingleLine, Format = ",", ValueConverter = typeof(ByteHexConverter))]
		public byte[] Subtypes;
		[IniCollection(IniCollectionMode.IndexOnly)]
		public Dictionary<string, string> CustomProperties;
	}

	[TypeConverter(typeof(SpriteFrameConverter))]
	public class SpriteFrame
	{
		public int OffX;
		public int OffY;
		public int Width;
		public int Height;
		public int X;
		public int Y;

		public SpriteFrame(string data)
		{
			string[] split = data.Split(',');
			OffX = int.Parse(split[0]);
			OffY = int.Parse(split[1]);
			Width = int.Parse(split[2]);
			Height = int.Parse(split[3]);
			X = int.Parse(split[4]);
			Y = int.Parse(split[5]);
		}

		public override string ToString() => $"{OffX}, {OffY}, {Width}, {Height}, {X}, {Y}";
	}

	public class SpriteFrameConverter : TypeConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(SpriteFrame))
				return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is SpriteFrame sf)
				return sf.ToString();
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string st)
				return new SpriteFrame(st);
			return base.ConvertFrom(context, culture, value);
		}
	}

	[TypeConverter(typeof(AnimDataConverter))]
	public class AnimData
	{
		public string File;
		public int Anim;
		public int Frame;

		public AnimData(string data)
		{
			string[] split = data.Split(':');
			File = split[0];
			Anim = int.Parse(split[1]);
			Frame = int.Parse(split[2]);
		}

		public override string ToString() => $"{File}:{Anim}:{Frame}";
	}

	public class AnimDataConverter : TypeConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(AnimData))
				return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is AnimData sf)
				return sf.ToString();
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string st)
				return new AnimData(st);
			return base.ConvertFrom(context, culture, value);
		}
	}

	public abstract class ObjectDefinition
	{
		public void Init(RSDKv3_4.GameConfig.ObjectInfo info)
		{
			Name = info.name;
			Script = info.script;
		}

		public abstract void Init(ObjectData data);
		public abstract ReadOnlyCollection<byte> Subtypes { get; }
		public abstract string SubtypeName(byte subtype);
		public abstract Sprite SubtypeImage(byte subtype);
		public string Name { get; private set; }
		public string Script { get; private set; }
		public virtual byte DefaultSubtype { get { return 0; } }
		public abstract Sprite Image { get; }
		public abstract Sprite GetSprite(ObjectEntry obj);
		public virtual Rectangle GetBounds(ObjectEntry obj) { return Rectangle.Empty; }
		public virtual Sprite GetDebugOverlay(ObjectEntry obj) { return null; }
		public virtual bool Debug { get { return false; } }
		public virtual bool Hidden { get { return false; } }
		static readonly PropertySpec[] specs = new PropertySpec[0];
		public virtual PropertySpec[] CustomProperties => specs;
	}

	/// <summary>
	/// Represents a single property in a PropertySpec.
	/// </summary>
	public class PropertySpec
	{
		private Attribute[] attributes;
		private string category;
		private object defaultValue;
		private string description;
		private string name;
		private Type type;
		private Type typeConverter;
		private Dictionary<string, int> @enum;
		private Func<ObjectEntry, object> getMethod;
		private Action<ObjectEntry, object> setMethod;

		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="type">The fully qualified name of the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the
		/// property grid.</param>
		/// <param name="description">A string that is displayed in the help area of the
		/// property grid.</param>
		/// <param name="defaultValue">The default value of the property, or null if there is
		/// no default value.</param>
		/// <param name="getMethod">The method called to get the value of the property.</param>
		/// <param name="setMethod">The method called to set the value of the property.</param>
		public PropertySpec(string name, string type, string category, string description, object defaultValue, Func<ObjectEntry, object> getMethod, Action<ObjectEntry, object> setMethod)
			: this(name, Type.GetType(type), category, description, defaultValue, getMethod, setMethod) { }

		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="type">A Type that represents the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the
		/// property grid.</param>
		/// <param name="description">A string that is displayed in the help area of the
		/// property grid.</param>
		/// <param name="defaultValue">The default value of the property, or null if there is
		/// no default value.</param>
		/// <param name="getMethod">The method called to get the value of the property.</param>
		/// <param name="setMethod">The method called to set the value of the property.</param>
		public PropertySpec(string name, Type type, string category, string description, object defaultValue, Func<ObjectEntry, object> getMethod, Action<ObjectEntry, object> setMethod)
		{
			this.name = name;
			this.type = type;
			this.category = category;
			this.description = description;
			this.defaultValue = defaultValue;
			this.attributes = null;
			this.getMethod = getMethod;
			this.setMethod = setMethod;
		}

		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="type">The fully qualified name of the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the
		/// property grid.</param>
		/// <param name="description">A string that is displayed in the help area of the
		/// property grid.</param>
		/// <param name="defaultValue">The default value of the property, or null if there is
		/// no default value.</param>
		/// <param name="typeConverter">The fully qualified name of the type of the type
		/// converter for this property.  This type must derive from TypeConverter.</param>
		/// <param name="getMethod">The method called to get the value of the property.</param>
		/// <param name="setMethod">The method called to set the value of the property.</param>
		public PropertySpec(string name, string type, string category, string description, object defaultValue, string typeConverter, Func<ObjectEntry, object> getMethod, Action<ObjectEntry, object> setMethod)
			: this(name, Type.GetType(type), category, description, defaultValue, Type.GetType(typeConverter), getMethod, setMethod) { }

		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="type">A Type that represents the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the
		/// property grid.</param>
		/// <param name="description">A string that is displayed in the help area of the
		/// property grid.</param>
		/// <param name="defaultValue">The default value of the property, or null if there is
		/// no default value.</param>
		/// <param name="typeConverter">The fully qualified name of the type of the type
		/// converter for this property.  This type must derive from TypeConverter.</param>
		/// <param name="getMethod">The method called to get the value of the property.</param>
		/// <param name="setMethod">The method called to set the value of the property.</param>
		public PropertySpec(string name, Type type, string category, string description, object defaultValue, string typeConverter, Func<ObjectEntry, object> getMethod, Action<ObjectEntry, object> setMethod) :
			this(name, type, category, description, defaultValue, Type.GetType(typeConverter), getMethod, setMethod) { }

		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="type">The fully qualified name of the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the
		/// property grid.</param>
		/// <param name="description">A string that is displayed in the help area of the
		/// property grid.</param>
		/// <param name="defaultValue">The default value of the property, or null if there is
		/// no default value.</param>
		/// <param name="typeConverter">The Type that represents the type of the type
		/// converter for this property.  This type must derive from TypeConverter.</param>
		/// <param name="getMethod">The method called to get the value of the property.</param>
		/// <param name="setMethod">The method called to set the value of the property.</param>
		public PropertySpec(string name, string type, string category, string description, object defaultValue, Type typeConverter, Func<ObjectEntry, object> getMethod, Action<ObjectEntry, object> setMethod) :
			this(name, Type.GetType(type), category, description, defaultValue, typeConverter, getMethod, setMethod) { }

		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="type">A Type that represents the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the
		/// property grid.</param>
		/// <param name="description">A string that is displayed in the help area of the
		/// property grid.</param>
		/// <param name="defaultValue">The default value of the property, or null if there is
		/// no default value.</param>
		/// <param name="typeConverter">The Type that represents the type of the type
		/// converter for this property.  This type must derive from TypeConverter.</param>
		/// <param name="getMethod">The method called to get the value of the property.</param>
		/// <param name="setMethod">The method called to set the value of the property.</param>
		public PropertySpec(string name, Type type, string category, string description, object defaultValue, Type typeConverter, Func<ObjectEntry, object> getMethod, Action<ObjectEntry, object> setMethod) :
			this(name, type, category, description, defaultValue, getMethod, setMethod)
		{
			this.typeConverter = typeConverter;
		}

		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="type">The fully qualified name of the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the
		/// property grid.</param>
		/// <param name="description">A string that is displayed in the help area of the
		/// property grid.</param>
		/// <param name="defaultValue">The default value of the property, or null if there is
		/// no default value.</param>
		/// <param name="enum">The enumeration used by the property.</param>
		/// <param name="getMethod">The method called to get the value of the property.</param>
		/// <param name="setMethod">The method called to set the value of the property.</param>
		public PropertySpec(string name, string type, string category, string description, object defaultValue, Dictionary<string, int> @enum, Func<ObjectEntry, object> getMethod, Action<ObjectEntry, object> setMethod) :
			this(name, Type.GetType(type), category, description, defaultValue, @enum, getMethod, setMethod) { }

		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="type">A Type that represents the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the
		/// property grid.</param>
		/// <param name="description">A string that is displayed in the help area of the
		/// property grid.</param>
		/// <param name="defaultValue">The default value of the property, or null if there is
		/// no default value.</param>
		/// <param name="enum">The enumeration used by the property.</param>
		/// <param name="getMethod">The method called to get the value of the property.</param>
		/// <param name="setMethod">The method called to set the value of the property.</param>
		public PropertySpec(string name, Type type, string category, string description, object defaultValue, Dictionary<string, int> @enum, Func<ObjectEntry, object> getMethod, Action<ObjectEntry, object> setMethod) :
			this(name, type, category, description, defaultValue, typeof(StringEnumConverter), getMethod, setMethod)
		{
			this.@enum = @enum;
		}

		/// <summary>
		/// Gets or sets a collection of additional Attributes for this property.  This can
		/// be used to specify attributes beyond those supported intrinsically by the
		/// PropertySpec class, such as ReadOnly and Browsable.
		/// </summary>
		public Attribute[] Attributes
		{
			get { return attributes; }
			set { attributes = value; }
		}

		/// <summary>
		/// Gets or sets the category name of this property.
		/// </summary>
		public string Category
		{
			get { return category; }
			set { category = value; }
		}

		/// <summary>
		/// Gets or sets the default value of this property.
		/// </summary>
		public object DefaultValue
		{
			get { return defaultValue; }
			set { defaultValue = value; }
		}

		/// <summary>
		/// Gets or sets the help text description of this property.
		/// </summary>
		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		/// <summary>
		/// Gets or sets the name of this property.
		/// </summary>
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		/// <summary>
		/// Gets or sets the type of this property.
		/// </summary>
		public Type Type
		{
			get { return type; }
			set { type = value; }
		}

		/// <summary>
		/// Gets or sets the type converter
		/// type for this property.
		/// </summary>
		public Type ConverterType
		{
			get { return typeConverter; }
			set { typeConverter = value; }
		}

		public object GetValue(ObjectEntry item)
		{
			return getMethod(item);
		}

		public void SetValue(ObjectEntry item, object value)
		{
			setMethod(item, value);
		}

		public Dictionary<string, int> Enumeration
		{
			get { return @enum; }
			set { @enum = value; }
		}
	}

	public class StringEnumConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			return base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(int))
				return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
			{
				Dictionary<string, int> values = ((PropertySpecDescriptor)context.PropertyDescriptor).Enumeration;
				if (values.ContainsKey((string)value))
					return values[(string)value];
				else
					return int.Parse((string)value, culture);
			}
			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is int)
			{
				Dictionary<string, int> values = ((PropertySpecDescriptor)context.PropertyDescriptor).Enumeration;
				if (values.ContainsValue((int)value))
					return values.GetKey((int)value);
				else
					return ((int)value).ToString(culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new StandardValuesCollection(((PropertySpecDescriptor)context.PropertyDescriptor).Enumeration.Keys);
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context.PropertyDescriptor is PropertySpecDescriptor;
		}
	}

	internal class PropertySpecDescriptor : PropertyDescriptor
	{
		private PropertySpec item;

		public PropertySpecDescriptor(PropertySpec item, string name, Attribute[] attrs) :
			base(name, attrs)
		{
			this.item = item;
		}

		public override Type ComponentType
		{
			get { return item.GetType(); }
		}

		public override bool IsReadOnly
		{
			get { return (Attributes.Matches(ReadOnlyAttribute.Yes)); }
		}

		public override Type PropertyType
		{
			get { return item.Type; }
		}

		public override bool CanResetValue(object component)
		{
			if (item.DefaultValue == null)
				return false;
			else
				return !this.GetValue(component).Equals(item.DefaultValue);
		}

		public override object GetValue(object component)
		{
			return item.GetValue((ObjectEntry)component);
		}

		public override void ResetValue(object component)
		{
			SetValue(component, item.DefaultValue);
		}

		public override void SetValue(object component, object value)
		{
			if (value is string s)
				value = (Enumeration.ContainsKey(s)) ? Enumeration[s] : int.Parse(s);
			
			item.SetValue((ObjectEntry)component, value);
		}

		public override bool ShouldSerializeValue(object component)
		{
			object val = this.GetValue(component);

			if (item.DefaultValue == null && val == null)
				return false;
			else
				return !val.Equals(item.DefaultValue);
		}

		public override TypeConverter Converter
		{
			get
			{
				if (item.ConverterType != null)
					return (TypeConverter)Activator.CreateInstance(item.ConverterType);
				return base.Converter;
			}
		}

		public override string Category { get { return item.Category; } }

		public override string Description { get { return item.Description; } }

		public Dictionary<string, int> Enumeration { get { return item.Enumeration; } }
	}

	public class DefaultObjectDefinition : ObjectDefinition
	{
		private Sprite[] spr = new Sprite[4];
		private byte defsub;
		private List<byte> subtypes = new List<byte>();
		bool debug = false;
		bool hidden = false;

		public override void Init(ObjectData data)
		{
			try
			{
				if (data.Anim != null)
				{
					spr[0] = LevelData.GetAnimationFrame(data.Anim.File, data.Anim.Anim, data.Anim.Frame);
					if (data.Priority)
						spr[0].InvertPriority();
				}
				else if (data.Sheet != null && data.Frame != null)
				{
					BitmapBits img = LevelData.GetSpriteSheet(data.Sheet);
					spr[0] = new Sprite(img.GetSection(data.Frame.X, data.Frame.Y, data.Frame.Width, data.Frame.Height), data.Frame.OffX, data.Frame.OffY);
					if (data.Priority)
						spr[0].InvertPriority();
				}
				else
				{
					spr[0] = LevelData.UnknownSprite;
					debug = true;
				}
			}
			catch (Exception ex)
			{
				LevelData.Log("Error loading object definition " + Name + ":", ex.ToString());
				spr[0] = LevelData.UnknownSprite;
				debug = true;
			}
			spr[1] = new Sprite(spr[0]);
			spr[1].Flip(true, false);
			spr[2] = new Sprite(spr[0]);
			spr[2].Flip(false, true);
			spr[3] = new Sprite(spr[1]);
			spr[3].Flip(false, true);
			defsub = data.DefaultSubtype;
			debug |= data.Debug;
			hidden = data.Hidden;
			if (data.Subtypes != null)
				subtypes.AddRange(data.Subtypes);
		}

		public override ReadOnlyCollection<byte> Subtypes { get { return new ReadOnlyCollection<byte>(subtypes); } }

		public override string SubtypeName(byte subtype) { return string.Empty; }

		public override Sprite SubtypeImage(byte subtype) { return Image; }

		public override byte DefaultSubtype { get { return defsub; } }

		public override Sprite Image { get { return spr[0]; } }

		public override Sprite GetSprite(ObjectEntry obj)
		{
			if (obj is V4ObjectEntry obj4)
				return spr[(int)obj4.Direction];
			return spr[0];
		}

		public override bool Debug { get { return debug; } }

		public override bool Hidden { get { return hidden; } }
	}

	public class XMLObjectDefinition : ObjectDefinition
	{
		class PropertyInfo
		{
			private Type type;
			private Dictionary<string, int> @enum;
			private Func<ObjectEntry, object> getMethod;
			private Action<ObjectEntry, object> setMethod;

			/// <summary>
			/// Initializes a new instance of the PropertyInfo class.
			/// </summary>
			/// <param name="type">The fully qualified name of the type of the property.</param>
			/// <param name="getMethod">The method called to get the value of the property.</param>
			/// <param name="setMethod">The method called to set the value of the property.</param>
			public PropertyInfo(string type, Func<ObjectEntry, object> getMethod, Action<ObjectEntry, object> setMethod)
				: this(Type.GetType(type), getMethod, setMethod) { }

			/// <summary>
			/// Initializes a new instance of the PropertyInfo class.
			/// </summary>
			/// <param name="type">A Type that represents the type of the property.</param>
			/// <param name="getMethod">The method called to get the value of the property.</param>
			/// <param name="setMethod">The method called to set the value of the property.</param>
			public PropertyInfo(Type type, Func<ObjectEntry, object> getMethod, Action<ObjectEntry, object> setMethod)
			{
				this.type = type;
				this.getMethod = getMethod;
				this.setMethod = setMethod;
			}

			/// <summary>
			/// Initializes a new instance of the PropertyInfo class.
			/// </summary>
			/// <param name="type">The fully qualified name of the type of the property.</param>
			/// <param name="enum">The enumeration used by the property.</param>
			/// <param name="getMethod">The method called to get the value of the property.</param>
			/// <param name="setMethod">The method called to set the value of the property.</param>
			public PropertyInfo(string type, Dictionary<string, int> @enum, Func<ObjectEntry, object> getMethod, Action<ObjectEntry, object> setMethod)
				: this(Type.GetType(type), @enum, getMethod, setMethod) { }

			/// <summary>
			/// Initializes a new instance of the PropertyInfo class.
			/// </summary>
			/// <param name="type">A Type that represents the type of the property.</param>
			/// <param name="enum">The enumeration used by the property.</param>
			/// <param name="getMethod">The method called to get the value of the property.</param>
			/// <param name="setMethod">The method called to set the value of the property.</param>
			public PropertyInfo(Type type, Dictionary<string, int> @enum, Func<ObjectEntry, object> getMethod, Action<ObjectEntry, object> setMethod)
				: this(type, getMethod, setMethod)
			{
				this.@enum = @enum;
			}

			/// <summary>
			/// Gets or sets the type of this property.
			/// </summary>
			public Type Type
			{
				get { return type; }
				set { type = value; }
			}

			public object GetValue(ObjectEntry item)
			{
				return getMethod(item);
			}

			public void SetValue(ObjectEntry item, object value)
			{
				setMethod(item, value);
			}

			public Dictionary<string, int> Enumeration { get { return @enum; } }
		}

		XMLDef.ObjDef xmldef;
		Dictionary<string, Sprite> images = new Dictionary<string, Sprite>();
		Dictionary<string, XMLDef.ImageRef[]> imagesets = new Dictionary<string, XMLDef.ImageRef[]>();
		PropertySpec[] customProperties = new PropertySpec[0];
		Dictionary<string, PropertyInfo> propertyInfo = new Dictionary<string, PropertyInfo>();
		Dictionary<string, Dictionary<string, int>> enums;

		public override void Init(ObjectData data)
		{
			xmldef = XMLDef.ObjDef.Load(data.XMLFile);
			if (xmldef.Images != null)
				foreach (XMLDef.Image item in xmldef.Images)
				{
					Sprite sprite = default(Sprite);
					switch (item)
					{
						case XMLDef.ImageFromSheet sheetimg:
							BitmapBits bmp = LevelData.GetSpriteSheet(sheetimg.sheet);
							sprite = new Sprite(bmp.GetSection(sheetimg.sourcex, sheetimg.sourcey, sheetimg.width, sheetimg.height), sheetimg.Offset.ToPoint());
							if (sheetimg.priority) sprite.InvertPriority();
							break;
						case XMLDef.ImageFromAnim animimg:
							sprite = LevelData.GetAnimationFrame(animimg.file, animimg.anim, animimg.frame);
							sprite.Offset(animimg.Offset.X, animimg.Offset.Y);
							if (data.Priority) sprite.InvertPriority();
							break;
					}
					images.Add(item.id, sprite);
				}
			if (xmldef.ImageSets != null)
				foreach (XMLDef.ImageSet set in xmldef.ImageSets)
					imagesets[set.id] = set.Images;
			if (xmldef.Subtypes == null)
				xmldef.Subtypes = new XMLDef.Subtype[0];
			if (xmldef.Enums != null)
			{
				enums = new Dictionary<string, Dictionary<string, int>>(xmldef.Enums.Length);
				foreach (XMLDef.Enum item in xmldef.Enums)
				{
					Dictionary<string, int> members = new Dictionary<string, int>(item.Items.Length);
					int value = 0;
					foreach (XMLDef.EnumMember mem in item.Items)
					{
						if (mem.valueSpecified)
							value = mem.value;
						members.Add(mem.name, value++);
					}
					enums.Add(item.name, members);
				}
			}
			else
				enums = new Dictionary<string, Dictionary<string, int>>();
			if (xmldef.Properties != null)
			{
				List<PropertySpec> custprops = new List<PropertySpec>(xmldef.Properties.Length);
				Dictionary<string, PropertyInfo> propinf = new Dictionary<string, PropertyInfo>(xmldef.Properties.Length);
				foreach (XMLDef.Property property in xmldef.Properties)
				{
					int mask = 0;
					int prop_startbit = property.startbit;
					for (int i = 0; i < property.length; i++)
						mask |= 1 << (property.startbit + i);
					Func<ObjectEntry, object> getMethod;
					Action<ObjectEntry, object> setMethod;
					string name = property.sourceSpecified ? property.source : "PropertyValue";
					System.Reflection.PropertyInfo prop = typeof(V4ObjectEntry).GetProperty(name);
					if (prop == null)
						continue;
					if (enums.ContainsKey(property.type))
					{
						if (prop.PropertyType == typeof(byte))
						{
							getMethod = (obj) => ((byte)prop.GetValue(obj) & mask) >> prop_startbit;
							setMethod = (obj, val) => prop.SetValue(obj, (byte)(((byte)prop.GetValue(obj) & ~mask) | (((int)val << prop_startbit) & mask)));
						}
						else
						{
							getMethod = (obj) => ((int)prop.GetValue(obj) & mask) >> prop_startbit;
							setMethod = (obj, val) => prop.SetValue(obj, (int)(((int)prop.GetValue(obj) & ~mask) | (((int)val << prop_startbit) & mask)));
						}
						custprops.Add(new PropertySpec(property.displayname ?? property.name, typeof(int), "Extended", property.description, null, enums[property.type], getMethod, setMethod));
						propinf.Add(property.name, new PropertyInfo(typeof(int), enums[property.type], getMethod, setMethod));
					}
					else
					{
						Type type = LevelData.ExpandTypeName(property.type);
						if (type != typeof(bool))
						{
							if (prop.PropertyType == typeof(byte))
							{
								getMethod = (obj) => ((byte)prop.GetValue(obj) & mask) >> prop_startbit;
								setMethod = (obj, val) => prop.SetValue(obj, (byte)(((byte)prop.GetValue(obj) & ~mask) | (((int)val << prop_startbit) & mask)));
							}
							else
							{
								getMethod = (obj) => ((int)prop.GetValue(obj) & mask) >> prop_startbit;
								setMethod = (obj, val) => prop.SetValue(obj, (int)(((int)prop.GetValue(obj) & ~mask) | (((int)val << prop_startbit) & mask)));
							}
						}
						else
						{
							if (prop.PropertyType == typeof(byte))
							{
								getMethod = (obj) => (((byte)prop.GetValue(obj) & mask) >> prop_startbit) != 0;
								setMethod = (obj, val) => prop.SetValue(obj, (byte)(((byte)prop.GetValue(obj) & ~mask) | (((bool)val ? 1 : 0) << prop_startbit)));
							}
							else
							{
								getMethod = (obj) => (((int)prop.GetValue(obj) & mask) >> prop_startbit) != 0;
								setMethod = (obj, val) => prop.SetValue(obj, (int)(((int)prop.GetValue(obj) & ~mask) | (((bool)val ? 1 : 0) << prop_startbit)));
							}
						}
						custprops.Add(new PropertySpec(property.displayname ?? property.name, type, "Extended", property.description, null, getMethod, setMethod));
						propinf.Add(property.name, new PropertyInfo(type, getMethod, setMethod));
					}
				}
				customProperties = custprops.ToArray();
				propertyInfo = propinf;
			}
		}

		private Sprite ReadImageSet(XMLDef.ImageRef[] refs)
		{
			List<Sprite> sprs = new List<Sprite>(refs.Length);
			foreach (XMLDef.ImageRef img in refs)
				sprs.Add(ReadImageRef(img));
			return new Sprite(sprs);
		}

		private Sprite ReadImageRef(XMLDef.ImageRef img)
		{
			bool xflip = false, yflip = false;
			switch (img.xflip)
			{
				case XMLDef.FlipType.ReverseFlip:
				case XMLDef.FlipType.AlwaysFlip:
					xflip = true;
					break;
			}
			switch (img.yflip)
			{
				case XMLDef.FlipType.ReverseFlip:
				case XMLDef.FlipType.AlwaysFlip:
					yflip = true;
					break;
			}
			int xoff = img.Offset.X;
			if (xflip)
				xoff = -xoff;
			int yoff = img.Offset.Y;
			if (yflip)
				yoff = -yoff;
			Sprite sp = new Sprite(images[img.image]);
			sp.Flip(xflip, yflip);
			sp.Offset(xoff, yoff);
			return sp;
		}

		private Sprite ReadImageRefList(XMLDef.ImageRefList list)
		{
			List<Sprite> sprs = new List<Sprite>();
			foreach (object item in list.Images)
				switch (item)
				{
					case XMLDef.ImageSetRef set:
						sprs.Add(ReadImageSet(imagesets[set.set]));
						break;
					case XMLDef.ImageRef img:
						sprs.Add(ReadImageRef(img));
						break;
				}
			return new Sprite(sprs);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(Array.ConvertAll(xmldef.Subtypes, (a) => a.subtype)); }
		}

		public override string SubtypeName(byte subtype)
		{
			foreach (XMLDef.Subtype item in xmldef.Subtypes)
				if (item.subtype == subtype)
					return item.name;
			return string.Empty;
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			foreach (XMLDef.Subtype item in xmldef.Subtypes)
				if (item.subtype == subtype)
					if (item.Images != null)
						return ReadImageRefList(item);
					else if (item.image != null)
						return images[item.image];
					else
						return LevelData.UnknownSprite;
			return LevelData.UnknownSprite;
		}

		public override Sprite Image
		{
			get
			{
				if (xmldef.DefaultImage != null && xmldef.DefaultImage.Images != null)
					return ReadImageRefList(xmldef.DefaultImage);
				else if (xmldef.Image != null)
					return images[xmldef.Image];
				else
					return SubtypeImage(DefaultSubtype);
			}
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			if (xmldef.Display != null)
			{
				foreach (XMLDef.DisplayOption option in xmldef.Display)
				{
					if (!CheckConditions(obj, option))
						continue;
					if (option.Images != null)
						return ReadImageRefList(option, obj);
					else
					{
						Sprite spr = new Sprite(LevelData.UnknownSprite);
						if (obj is V4ObjectEntry obj4)
							spr.Flip(obj4.Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX), obj4.Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY));
						return spr;
					}
				}
			}
			else if (xmldef.Subtypes != null)
			{
				foreach (XMLDef.Subtype item in xmldef.Subtypes)
				{
					if (obj.PropertyValue == item.subtype)
						if (item.Images != null)
							return ReadImageRefList(item, obj);
						else if (item.image != null)
						{
							Sprite spr = new Sprite(images[item.image]);
							if (obj is V4ObjectEntry obj4)
								spr.Flip(obj4.Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX), obj4.Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY));
							return spr;
						}
						else
						{
							Sprite spr = new Sprite(LevelData.UnknownSprite);
							if (obj is V4ObjectEntry obj4)
								spr.Flip(obj4.Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX), obj4.Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY));
							return spr;
						}
				}
			}
			Sprite sprite;
			if (xmldef.DefaultImage != null && xmldef.DefaultImage.Images != null)
				return ReadImageRefList(xmldef.DefaultImage, obj);
			else if (xmldef.Image != null)
				sprite = new Sprite(images[xmldef.Image]);
			else
				sprite = LevelData.UnknownSprite;
			if (obj is V4ObjectEntry _obj4)
				sprite.Flip(_obj4.Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX), _obj4.Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY));
			return sprite;
		}

		private Sprite ReadImageSet(XMLDef.ImageRef[] refs, ObjectEntry obj)
		{
			List<Sprite> sprs = new List<Sprite>(refs.Length);
			foreach (XMLDef.ImageRef img in refs)
				sprs.Add(ReadImageRef(img, obj));
			return new Sprite(sprs);
		}

		private Sprite ReadImageRef(XMLDef.ImageRef img, ObjectEntry obj)
		{
			bool xflip = false, yflip = false;
			if (obj is V4ObjectEntry obj4)
			{
				xflip = obj4.Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX);
				yflip = obj4.Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipY);
			}
			switch (img.xflip)
			{
				case XMLDef.FlipType.ReverseFlip:
					xflip = !xflip;
					break;
				case XMLDef.FlipType.NeverFlip:
					xflip = false;
					break;
				case XMLDef.FlipType.AlwaysFlip:
					xflip = true;
					break;
			}
			switch (img.yflip)
			{
				case XMLDef.FlipType.ReverseFlip:
					yflip = !yflip;
					break;
				case XMLDef.FlipType.NeverFlip:
					yflip = false;
					break;
				case XMLDef.FlipType.AlwaysFlip:
					yflip = true;
					break;
			}
			int xoff = img.Offset.X;
			if (xflip)
				xoff = -xoff;
			int yoff = img.Offset.Y;
			if (yflip)
				yoff = -yoff;
			Sprite sp = new Sprite(images[img.image]);
			sp.Flip(xflip, yflip);
			sp.Offset(xoff, yoff);
			return sp;
		}

		private Sprite ReadImageRefList(XMLDef.ImageRefList list, ObjectEntry obj)
		{
			List<Sprite> sprs = new List<Sprite>();
			foreach (object item in list.Images)
				switch (item)
				{
					case XMLDef.ImageSetRef set:
						sprs.Add(ReadImageSet(imagesets[set.set], obj));
						break;
					case XMLDef.ImageRef img:
						sprs.Add(ReadImageRef(img, obj));
						break;
				}
			return new Sprite(sprs);
		}

		private bool CheckConditions(ObjectEntry obj, XMLDef.DisplayOption option)
		{
			if (option.Conditions != null)
				foreach (XMLDef.Condition cond in option.Conditions)
				{
					if (propertyInfo.ContainsKey(cond.property))
					{
						PropertyInfo prop = propertyInfo[cond.property];
						object value = prop.GetValue(obj);
						if (prop.Enumeration != null)
						{
							if ((int)value != prop.Enumeration[cond.value])
								return false;
						}
						else
						{
							if (!object.Equals(value, prop.Type.InvokeMember("Parse", System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static, null, null, new[] { cond.value })))
								return false;
						}
					}
					else
					{
						System.Reflection.PropertyInfo prop = obj.GetType().GetProperty(cond.property);
						object value = prop.GetValue(obj, null);
						if (!object.Equals(value, prop.PropertyType.InvokeMember("Parse", System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static, null, null, new[] { cond.value })))
							return false;
					}
				}
			return true;
		}

		public override bool Debug
		{
			get { return xmldef.Debug; }
		}

		public override bool Hidden
		{
			get { return xmldef.Hidden; }
		}

		public override byte DefaultSubtype
		{
			get { return xmldef.DefaultSubtypeValue; }
		}

		public override PropertySpec[] CustomProperties
		{
			get { return customProperties; }
		}
	}
}
